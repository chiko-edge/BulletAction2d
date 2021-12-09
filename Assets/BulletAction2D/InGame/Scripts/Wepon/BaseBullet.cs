using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D rb = null;

    //�e��
    private float moveSpeed;

    //�˒�
    private float moveRange;
    //�e��


    //�e���


    //���e����


    //����

    private Vector2 velocity = Vector2.right;
    private Vector2 startPosition = Vector2.right;


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



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5;
        moveRange = 5;

    }

    // Update is called once per frame
    void Update()
    {
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + velocity);

        if (Vector2.Distance(transform.position,StartPosition) > moveRange)
        {
            Destroy(this.gameObject);
        }

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
    public static BaseBullet Instantiate(BaseBullet prefab,Vector2 target, Vector3 position)
    {
        BaseBullet obj = Instantiate(prefab,position,Quaternion.identity);
        obj.Velocity = target;
        obj.StartPosition = position;


        //������ݒ�
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);

        Debug.Log(obj.velocity);

        return obj;
    }
}
