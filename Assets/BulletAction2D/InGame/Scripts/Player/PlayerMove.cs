using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigidbody = null;
    
    //移動速度
    private float moveSpeed;
    //X方向の移動方向　＋1,0,ー1が入る
    private float moveDirection;
    //x方向の最終的な移動速度
    private float xSpeed;


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
        rigidbody = GetComponent<Rigidbody2D>();
        moveDirection = 0;
        moveSpeed = 5;
    }


    private void FixedUpdate()
    {

        //x方向の移動速度を設定
        xSpeed = moveSpeed * moveDirection;

        rigidbody.velocity = new Vector2(xSpeed, rigidbody.velocity.y);
    }



}
