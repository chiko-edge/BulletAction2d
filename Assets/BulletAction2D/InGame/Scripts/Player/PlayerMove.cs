using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private GroundCheck groundCheck = null;
    private HeadCheck headCheck = null;
    
    //�ړ����x
    private float moveSpeed;
    //X�����̈ړ������@�{1,0,�[1������
    private float moveDirection;
    //x�����̍ŏI�I�Ȉړ����x
    private float xSpeed;
    //y�����̍ŏI�I�Ȉړ����x
    private float ySpeed;
    //�d��
    private float gravity;
    //�W�����v�X�s�[�h
    private float jumpSpeed = 10;
    //��������
    private float jumpHight = 50;
    //�W�����v���Ԑ���
    private float jumpLimitTime = 0.3f;

    //�W�����v�t���O
    private bool isJump;
    //�W�����v�J�n�n�_
    private float jumpPos;
    //�W�����v����
    private float jumpTime;

    //��ׂ鍂����
    private bool canHeight;
    //�W�����v���Ԃ͑��v��
    private bool canTime;



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
        rb = GetComponent<Rigidbody2D>();
        moveDirection = 0;
        moveSpeed = 5;
        groundCheck = GetComponentInChildren<GroundCheck>();
        headCheck = GetComponentInChildren<HeadCheck>();
        gravity = 9.8f;

        
    }


    private void FixedUpdate()
    {
        //x�����̈ړ����x��ݒ�
        xSpeed = moveSpeed * moveDirection;
        ySpeed = 0;

        Debug.Log(headCheck.IsHead());

        if (isJump)
        {
            canHeight = jumpPos + jumpHight > transform.position.y;
            canTime = jumpLimitTime > jumpTime;

            if(canHeight && canTime && !headCheck.IsHead())
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
            }
        }
        else if(!groundCheck.IsGround())
        {
            ySpeed = -gravity;
        }



        rb.velocity = new Vector2(xSpeed, ySpeed);
    }


    /// <summary>
    /// �W�����v�J�n
    /// </summary>
    public void jumpStart()
    {
        if (groundCheck.IsGround())
        {
            isJump = true;
            jumpPos = transform.position.y;
            jumpTime = 0.0f;
        }

    }

    /// <summary>
    /// �W�����v�I��
    /// </summary>
    public void jumpEnd()
    {
        isJump = false;
    }


}
