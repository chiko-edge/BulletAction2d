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
    private Vector2 startPosition = Vector2.right;

    //�ړ����x
    private float moveSpeed = default;
    //�����x
    private float acceleration = default;
    //�ړ�����
    private float moveDistance = default;



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
        Velocity = velocityMake();

        //�ړ����{
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + velocity);

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
    public static BaseBullet Instantiate(BaseBullet prefab,Vector2 target, Vector3 position, BulletData bulletData)
    {
        BaseBullet obj = Instantiate(prefab,position,Quaternion.identity);
        obj.Velocity = target;
        obj.StartPosition = position;
        obj.Bullet = bulletData;


        //������ݒ�
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);

        return obj;
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
