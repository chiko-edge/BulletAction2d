using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D rb = null;


    //射程

    //弾道

    //弾速
    private float moveSpeed;

    //弾種別


    //着弾動作


    //効果

    Vector2 velocity = Vector2.right;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + velocity);
    }





    /// <summary>
    /// インスタンス化と初期化処理を一緒にする
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="target"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static BaseBullet Instantiate(BaseBullet prefab,Vector2 target, Vector3 position)
    {
        BaseBullet obj = Instantiate(prefab,position,Quaternion.identity);
        obj.velocity = target;

        //向きを設定
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);

        Debug.Log(obj.velocity);

        return obj;
    }
}
