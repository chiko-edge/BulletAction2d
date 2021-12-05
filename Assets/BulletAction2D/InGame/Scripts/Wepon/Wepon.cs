using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    private BulletCircle bulletCircle = null;
    
    [SerializeField]
    private BaseBullet baseBullet;


    private void Start()
    {
        bulletCircle = GetComponentInChildren<BulletCircle>();
    }

    public void SetCircle(Vector2 position)
    {
        bulletCircle.SetCirclePos(position);
    }

    public void Attack()
    {
        //Æ€‚Æ•ŠíˆÊ’u‚©‚çŒü‚«‚ğİ’è‚·‚é
        Vector3 targetVecter3 = (bulletCircle.transform.position - transform.position).normalized;
        Vector2 targetVecter = new Vector2(targetVecter3.x, targetVecter3.y);
        BaseBullet obj = BaseBullet.Instantiate(baseBullet, targetVecter, transform.position);
    }
}
