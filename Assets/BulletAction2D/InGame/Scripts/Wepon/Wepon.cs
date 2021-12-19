using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    private BulletCircle bulletCircle = null;
    
    [SerializeField]
    private BaseBullet baseBullet;

    //���L���X�g�^�C��
    private float recastTime = 0.2f;
    //�U�����s�\�t���O
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

            //�Ə��ƕ���ʒu���������ݒ肷��
            Vector3 targetVecter3 = (bulletCircle.transform.position - transform.position).normalized;
            Vector2 targetVecter = new Vector2(targetVecter3.x, targetVecter3.y);
            BaseBullet obj = BaseBullet.Instantiate(baseBullet, targetVecter, transform.position, bulletData);

            canAttack = false;
            StartCoroutine("recast");
        }

    }

    //���L���X�g���Ԍo�ߌ�U���\�ɂ���
    IEnumerator recast()
    {
        yield return new WaitForSeconds(recastTime);
        canAttack = true;

    }
}
