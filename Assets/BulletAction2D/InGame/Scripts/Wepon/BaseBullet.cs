using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D rb = null;

    [SerializeField]
    private BulletData defaltBulletData;

    //���e�̏��
    private BulletData bullet;
    //�g�p����摜
    private SpriteRenderer spriteRenderer;


    private Vector2 velocity = Vector2.right;
    private Vector2 updateVelocity = Vector2.right;
    private Vector2 startPosition = Vector2.right;

    //�ړ����x
    private float moveSpeed = default;
    //�����x
    private float acceleration = default;
    //�ړ�����
    private float moveDistance = default;

    //wave�̂Ƃ��Ɏg�p����
    private Vector2 waveStarPoint;
    private Vector2 waveEndPoint = Vector2.zero;
    private Vector2 waveInterimPoint;
    private float waveTime = 0;
    private float switchVal = 1;
    private int useCount = 0;
    private int count = 0;
    private List<Vector2> l1 = new List<Vector2>();

    //�v���p�e�B�Q
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


        l1.Add(new Vector2(transform.position.x, transform.position.y));
        if (bullet.BulletBallisticType == BulletBallisticType.Wave)
        {
            while (Vector2.Distance(l1[count], StartPosition) < (bullet.maxMoveRange * 1.5f))
            {
                count++;
                l1.Add(startPosition + velocity.normalized * (bullet.moveSpeed * count));
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�ő�ړ������𒴂��Ă�����폜
        moveDistance = Vector2.Distance(transform.position, StartPosition);
        if (moveDistance > bullet.maxMoveRange)
        {
            Destroy(this.gameObject);
        }

        //�ړ����쐬
        moveSpeedMake();
        updateVelocity = velocityMake();

        //�ړ����{
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + updateVelocity);

    }

    

    /// <summary>
    /// �ڐG����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����ɓ�����Ώ�����
        Destroy(this.gameObject);
    }





    /// <summary>
    /// �C���X�^���X���Ə������������ꏏ�ɂ���
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
    /// �P���̒e���쐬����
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

        //������ݒ�
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);
    }

    /// <summary>
    /// �����̒e���쐬����
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

        //��̏ꍇ�͐��ʂɌ��Ă�悤�ɂ���
        if (bulletData.bulletQuantity / 2 != 0)
        {
            targetList.Add(target);
            count++;
        }

        while (count < bulletData.bulletQuantity)
        {
            bulletAngle += bulletData.bulletAngle;

            //+��-�̊p�x�����
            angleAxis = Quaternion.AngleAxis(bulletAngle, targetVecter3);
            targetList.Add(angleAxis * target);

            angleAxis = Quaternion.AngleAxis(-bulletAngle, targetVecter3);
            targetList.Add(angleAxis * target);

            count += 2;

        }

        //�e����
        foreach (Vector2 tar in targetList)
        {
            BaseBullet obj = Instantiate(prefab, position, Quaternion.identity);
            obj.Velocity = tar;
            obj.StartPosition = position;
            obj.Bullet = bulletData;

            //������ݒ�
            obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);
        }


    }


    /// <summary>
    /// �ړ������v�Z
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

            case BulletBallisticType.Wave:

                if(waveTime >= 1)
                {
                    waveTime = 0;
                    switchVal = -switchVal;
                    useCount++;
                }

                if(waveTime == 0)
                {
                    //�n�_�ݒ�
                    waveStarPoint = l1[useCount];
                    //�I�_�ݒ�
                    waveEndPoint = l1[useCount + 1];
                    //���Ԓn�_�ݒ�
                    waveInterimPoint = Vector2.Lerp(waveStarPoint, waveEndPoint, 0.5f) + switchVal * Vector2.Perpendicular(velocity).normalized * bullet.waveWidth;

                }

                //�ݒ肵���I�_�܂ňړ�������
                waveTime = (waveTime + Time.deltaTime) >= 1 ? 1 : waveTime + Time.deltaTime;

                Vector2 start = Vector2.Lerp(waveStarPoint, waveInterimPoint, waveTime);
                Vector2 end = Vector2.Lerp(waveInterimPoint, waveEndPoint, waveTime);

                result = Vector2.Lerp(start, end, waveTime) - new Vector2(transform.position.x,transform.position.y);


                break;
        }

        return result;
    }

    /// <summary>
    /// �ړ����x�v�Z
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
