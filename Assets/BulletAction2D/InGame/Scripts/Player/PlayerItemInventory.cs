using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInventory : MonoBehaviour
{
    [SerializeField]
    private List<BulletData> bulletDatas = new List<BulletData>();
    private GameObject tempBulletItem = null;

    private int activBulletIndex = 0;

    private const int MaxBulletIndex = 8;

    private const int MaxActiveBulletIndex = 3;


    /// <summary>
    /// アイテム取得ボタン押されたときに実施
    /// 弾のイベントリ情報に入れる
    /// </summary>
    public void setBulletData()
    {
        if(tempBulletItem != null)
        {
            bulletDatas.Add(tempBulletItem.GetComponent<BulletItem>().Data);
            Destroy(tempBulletItem);

            

        }
    }

    public void changeActiveBulletIndex()
    {
        activBulletIndex++;

        if(bulletDatas.Count >= MaxActiveBulletIndex)
        {
            activBulletIndex %= MaxActiveBulletIndex;
        }
        else
        {
            activBulletIndex %= bulletDatas.Count;
        }

        Debug.Log("index : "+activBulletIndex);
    }

    public BulletData getActiveBullet()
    {
        if(bulletDatas.Count < 1)
        {
            return null;
        }

        return bulletDatas[activBulletIndex];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletItem" && tempBulletItem == null)
        {
            tempBulletItem = collision.gameObject;
            
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletItem" && tempBulletItem != null)
        {
            tempBulletItem = null;
        }
    }
}
