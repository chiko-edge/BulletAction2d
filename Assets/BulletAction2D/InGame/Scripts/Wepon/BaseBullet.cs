using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D rb = null;

    //’e‘¬
    private float moveSpeed;

    //Ë’ö
    private float moveRange;
    //’e“¹


    //’eí•Ê


    //’…’e“®ì


    //Œø‰Ê

    private Vector2 velocity = Vector2.right;
    private Vector2 startPosition = Vector2.right;


    //ƒvƒƒpƒeƒBŒQ
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
    /// ÚG”»’è
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //‰½‚©‚É“–‚½‚ê‚ÎÁ‚¦‚é
        Destroy(this.gameObject);
    }





    /// <summary>
    /// ƒCƒ“ƒXƒ^ƒ“ƒX‰»‚Æ‰Šú‰»ˆ—‚ğˆê‚É‚·‚é
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


        //Œü‚«‚ğİ’è
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, target);

        Debug.Log(obj.velocity);

        return obj;
    }
}
