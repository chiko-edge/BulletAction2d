using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D rb = null;

    [SerializeField]
    private BulletData defaltBulletData;

    private BulletData bullet;

    private SpriteRenderer spriteRenderer;


    //弾道


    //弾種別


    //着弾動作


    //効果

    private Vector2 velocity = Vector2.right;
    private Vector2 startPosition = Vector2.right;


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
    void Update()
    {
        velocity = velocity.normalized * bullet.moveSpeed * Time.deltaTime;

        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + velocity);

        if (Vector2.Distance(transform.position,StartPosition) > bullet.maxMoveRange)
        {
            Destroy(this.gameObject);
        }

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
    public static BaseBullet Instantiate(BaseBullet prefab,Vector2 target, Vector3 position, BulletData bulletData)
    {
        BaseBullet obj = Instantiate(prefab,position,Quaternion.identity);
        obj.Velocity = target;
        obj.StartPosition = position;
        obj.Bullet = bulletData;


        //向きを設定
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);

        Debug.Log(obj.velocity);

        return obj;
    }
}
