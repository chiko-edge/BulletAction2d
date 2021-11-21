using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigidbody = null;
    
    //�ړ����x
    private float moveSpeed;
    //X�����̈ړ������@�{1,0,�[1������
    private float moveDirection;
    //x�����̍ŏI�I�Ȉړ����x
    private float xSpeed;


    //�v���p�e�B�Q
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value;  }
    }




    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveDirection = 0;
        moveSpeed = 5;
    }


    private void FixedUpdate()
    {

        //x�����̈ړ����x��ݒ�
        xSpeed = moveSpeed * moveDirection;

        rigidbody.velocity = new Vector2(xSpeed, rigidbody.velocity.y);
    }



}
