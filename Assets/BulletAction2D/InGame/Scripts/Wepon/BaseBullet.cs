using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D rb = null;

    [SerializeField]
    private BulletData defaltBulletData;

    //撃つ弾の情報
    private BulletData bullet;
    //使用する画像
    private SpriteRenderer spriteRenderer;


    private Vector2 velocity = Vector2.right;
    private Vector2 updateVelocity = Vector2.right;
    private Vector2 startPosition = Vector2.right;

    //移動速度
    private float moveSpeed = default;
    //加速度
    private float acceleration = default;
    //移動距離
    private float moveDistance = default;

    float sec = default;

    //プロパティ群
    public Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public Vector2 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    public BulletData Bullet
    {
        get { return bullet; }
        set { 
            if(value == null)
            {
                bullet = defaltBulletData;
            }
            else
            {
                bullet = value;
            } 
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bullet.bulletSprite;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //最大移動距離を超えていたら削除
        moveDistance = Vector2.Distance(transform.position, StartPosition);
        if (moveDistance > bullet.maxMoveRange)
        {
            Destroy(this.gameObject);
        }

        //移動情報作成
        moveSpeedMake();
        updateVelocity = velocityMake();

        //移動実施
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + updateVelocity);

    }

    

    /// <summary>
    /// 接触判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //何かに当たれば消える
        Destroy(this.gameObject);
    }





    /// <summary>
    /// インスタンス化と初期化処理を一緒にする
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="target"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static void Instantiate(BaseBullet prefab, Vector2 target, Vector3 position, BulletData bulletData)
    {

        switch (bulletData.bulletType)
        {
            case BulletType.SingleBullet:
                SingleBulletInstantiate(prefab, target, position, bulletData);
                break;

            case BulletType.MultiBullet:
                MultiBulletInstantiate(prefab, target, position, bulletData);
                break;
        }
    }

    /// <summary>
    /// 単発の弾を作成する
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="target"></param>
    /// <param name="position"></param>
    /// <param name="bulletData"></param>
    private static void SingleBulletInstantiate(BaseBullet prefab, Vector2 target, Vector3 position, BulletData bulletData)
    {
        BaseBullet obj = Instantiate(prefab, position, Quaternion.identity);
        obj.Velocity = target;
        obj.StartPosition = position;
        obj.Bullet = bulletData;

        //向きを設定
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);
    }

    /// <summary>
    /// 複数の弾を作成する
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="target"></param>
    /// <param name="position"></param>
    /// <param name="bulletData"></param>
    private static void MultiBulletInstantiate(BaseBullet prefab, Vector2 target, Vector3 position, BulletData bulletData)
    {
        List<Vector2> targetList = new List<Vector2>();
        int count = 0;
        float bulletAngle = 0;
        Quaternion angleAxis;
        Vector3 targetVecter3 = new Vector3(target.x, target.y, 1);

        //奇数の場合は正面に撃てるようにする
        if (bulletData.bulletQuantity / 2 != 0)
        {
            targetList.Add(target);
            count++;
        }

        while (count < bulletData.bulletQuantity)
        {
            bulletAngle += bulletData.bulletAngle;

            //+と-の角度分作る
            angleAxis = Quaternion.AngleAxis(bulletAngle, targetVecter3);
            targetList.Add(angleAxis * target);

            angleAxis = Quaternion.AngleAxis(-bulletAngle, targetVecter3);
            targetList.Add(angleAxis * target);

            count += 2;

        }

        //弾発射
        foreach (Vector2 tar in targetList)
        {
            BaseBullet obj = Instantiate(prefab, position, Quaternion.identity);
            obj.Velocity = tar;
            obj.StartPosition = position;
            obj.Bullet = bulletData;

            //向きを設定
            obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);
        }


    }


    /// <summary>
    /// 移動方向計算
    /// </summary>
    /// <returns></returns>
    private Vector2 velocityMake()
    {
        Vector2 result;
        switch (bullet.BulletBallisticType)
        {
            case BulletBallisticType.Straight:
            default:
                result = Velocity.normalized * (moveSpeed + acceleration) * Time.deltaTime;
                break;

            case BulletBallisticType.Gravity:
                Velocity = new Vector2(Velocity.x, Velocity.y - bullet.downForce * Time.deltaTime);
                result = Velocity.normalized * (moveSpeed + acceleration) * Time.deltaTime;
                gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, Velocity);

                break;
        }

        return result;
    }

    /// <summary>
    /// 移動速度計算
    /// </summary>
    private void moveSpeedMake()
    {
        switch(bullet.bulletAccelerationType)
        {
            case BulletAccelerationType.ConstantVelocity:
            default:
                moveSpeed = bullet.moveSpeed;
                break;

            case BulletAccelerationType.Acceleration:
                moveSpeed = bullet.moveSpeed;
                if (moveDistance > bullet.starAccelerationDistance)
                {
                    acceleration += bullet.acceleration;
                    if(acceleration > bullet.maxAcceleration)
                    {
                        acceleration = bullet.maxAcceleration;
                    }
                }


                break;

            case BulletAccelerationType.Deceleration:
                moveSpeed = bullet.moveSpeed;
                if (moveDistance > bullet.starAccelerationDistance)
                {
                    acceleration -= bullet.acceleration;
                    if (acceleration < bullet.maxAcceleration)
                    {
                        acceleration = -bullet.maxAcceleration;
                    }
                }

                break;
        }
    }
}
