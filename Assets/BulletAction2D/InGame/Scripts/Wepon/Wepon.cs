using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    private BulletCircle bulletCircle = null;
    
    [SerializeField]
    private BaseBullet baseBullet;

    //リキャストタイム
    private float recastTime = 0.2f;
    //攻撃実行可能フラグ
    private bool canAttack;


    private void Start()
    {

        canAttack = true;
        bulletCircle = GetComponentInChildren<BulletCircle>();
    }

    public void SetCircle(Vector2 position)
    {
        bulletCircle.SetCirclePos(position);
    }

    public void Attack(BulletData bulletData)
    {
        if (canAttack)
        {

            //照準と武器位置から向きを設定する
            Vector3 targetVecter3 = (bulletCircle.transform.position - transform.position).normalized;
            Vector2 targetVecter = new Vector2(targetVecter3.x, targetVecter3.y);
            BaseBullet obj = BaseBullet.Instantiate(baseBullet, targetVecter, transform.position, bulletData);

            canAttack = false;
            StartCoroutine("recast");
        }

    }

    //リキャスト時間経過後攻撃可能にする
    IEnumerator recast()
    {
        yield return new WaitForSeconds(recastTime);
        canAttack = true;

    }
}
