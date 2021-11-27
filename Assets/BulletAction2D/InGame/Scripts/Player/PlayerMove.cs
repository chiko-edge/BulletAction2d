using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private GroundCheck groundCheck = null;
    private HeadCheck headCheck = null;
    
    //移動速度
    private float moveSpeed;
    //X方向の移動方向　＋1,0,ー1が入る
    private float moveDirection;
    //x方向の最終的な移動速度
    private float xSpeed;
    //y方向の最終的な移動速度
    private float ySpeed;
    //重力
    private float gravity;
    //ジャンプスピード
    private float jumpSpeed = 10;
    //高さ制限
    private float jumpHight = 50;
    //ジャンプ時間制限
    private float jumpLimitTime = 0.3f;

    //ジャンプフラグ
    private bool isJump;
    //ジャンプ開始地点
    private float jumpPos;
    //ジャンプ時間
    private float jumpTime;

    //飛べる高さか
    private bool canHeight;
    //ジャンプ時間は大丈夫か
    private bool canTime;



    //プロパティ群
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
        //x方向の移動速度を設定
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
    /// ジャンプ開始
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
    /// ジャンプ終了
    /// </summary>
    public void jumpEnd()
    {
        isJump = false;
    }


}
