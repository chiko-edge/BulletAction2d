using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerItemInventory : MonoBehaviour
{
    [SerializeField]
    private List<BulletData> bulletDatas = new List<BulletData>();
    private GameObject tempBulletItem = null;

    [SerializeField]
    private GameObject inventoryUi;
    private bool inventoryViewFlg;

    
    private GameObject[] bakItems = new GameObject[16];
    private GameObject[] bulletItems = new GameObject[4];



    private int activBulletIndex = 0;

    private const int MaxBulletIndex = 8;

    private const int MaxActiveBulletIndex = 3;


    private void Awake()
    {
        inventoryViewFlg = false;
        //インベントリ非表示
        inventoryUi.SetActive(false);

        foreach (Transform childTransFrom in inventoryUi.transform)
        {
            foreach(Transform child in childTransFrom)
            {
                foreach (Transform item in child)
                {
                    Debug.Log(item.gameObject.name.Substring(item.gameObject.name.IndexOf("_")+1));

                    if (childTransFrom.name.Equals("Bag"))
                    {
                        int i = Convert.ToInt32(item.gameObject.name.Substring(item.gameObject.name.IndexOf("_") + 1)) - 1;
                        bakItems[i] = item.gameObject;
                    }
                    else if (childTransFrom.name.Equals("Bullet"))
                    {
                        int i = Convert.ToInt32(item.gameObject.name.Substring(item.gameObject.name.IndexOf("_") + 1)) - 1;
                        bulletItems[i] = item.gameObject;
                    }
                }
            }
        }
        

    }

    /// <summary>
    /// アイテム取得ボタン押されたときに実施
    /// 弾のイベントリ情報に入れる
    /// </summary>
    public void setBulletData()
    {
        if(tempBulletItem != null)
        {
            if (bulletDatas.Count <= 19)
            {
                bulletDatas.Add(tempBulletItem.GetComponent<BulletItem>().Data);
                Destroy(tempBulletItem);

                bulletItems[0].transform.GetChild(0).GetComponent<Image>().sprite = bulletDatas[1].bulletIconSprite;
            }

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


    public void InventoryView()
    {
        inventoryViewFlg = !inventoryViewFlg;
        //インベントリ表示切り替え
        inventoryUi.SetActive(inventoryViewFlg) ;

        if (inventoryViewFlg)
        {
            Time.timeScale = 0f;
        }else
        {
            Time.timeScale = 1f;
        }
    }
}
