using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.EventSystems;


public class PlayerItemInventory : MonoBehaviour
{
    [SerializeField]
    private List<BulletData> bulletDatas = new List<BulletData>();
    private GameObject tempBulletItem = null;

    [SerializeField]
    private GameObject inventoryUi;
    private GameObject inventoryUiObject;
    private bool inventoryViewFlg;
    private CanvasGroup canvasGroup;
    private GameObject canvas;

    
    private GameObject[] bakItems = new GameObject[16];
    private GameObject[] bulletItems = new GameObject[4];



    private int activBulletIndex = 0;
    private const int MaxBulletIndex = 4;
    private const int MaxActiveBulletIndex = 4;

    //マウス位置
    private Vector2 mousePosition;
    private Vector2 mouseScreenPosition;

    private List<RaycastResult> results;

    private Camera mainCamera;
    private PointerEventData pointer;

    public Vector2 MousePosition
    {
        get { return mousePosition; }
        set { mousePosition = value; }
    }


    private void Awake()
    {
        results = new List<RaycastResult>();
        mainCamera = Camera.main;
        pointer = new PointerEventData(EventSystem.current);

        inventoryViewFlg = false;
        canvas = GameObject.Find("Canvas");

        //キャンバスにインベントリを作成
        inventoryUiObject = Instantiate(inventoryUi);
        inventoryUiObject.transform.SetParent(canvas.transform, false);

        canvasGroup = inventoryUiObject.GetComponent<CanvasGroup>();
        //インベントリ非表示
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;

        //uiのオブジェクトを設定
        foreach (Transform childTransFrom in inventoryUiObject.transform)
        {
            foreach(Transform child in childTransFrom)
            {
                foreach (Transform item in child)
                {

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

        setBulletImage();


    }

    private void Update()
    {
        if (inventoryViewFlg)
        {
            mouseScreenPosition = mainCamera.WorldToScreenPoint(mousePosition);
            pointer.position = mousePosition;
            EventSystem.current.RaycastAll(pointer, results);

            if(results.Count > 1)
            {
                foreach(RaycastResult target in results)
                {
                    Debug.Log(target.gameObject.name);
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
            if (bulletDatas.Count < MaxBulletIndex)
            {
                bulletDatas.Add(tempBulletItem.GetComponent<BulletItem>().Data);
                Destroy(tempBulletItem);

                setBulletImage();
            }

        }

    }

    /// <summary>
    /// インベントリに弾の画像をセットする
    /// </summary>
    private void setBulletImage()
    {
        for (int i = 0; i < bulletDatas.Count; i++)
        {
            bulletItems[i].transform.GetChild(0).GetComponent<Image>().sprite = bulletDatas[i].bulletIconSprite;
        }
    }

    /// <summary>
    /// 撃つ弾を設定する
    /// </summary>
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

    /// <summary>
    /// 撃てる弾の情報を取得
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// インベントリの表示切り替え
    /// 表示中は時間を止める
    /// </summary>
    /// <param name="playerStatus"></param>
    public void InventoryView(PlayerStatus playerStatus)
    {
        inventoryViewFlg = !inventoryViewFlg;
        canvasGroup.interactable = inventoryViewFlg;

        if (inventoryViewFlg)
        {
            playerStatus.Status = PlayerStatusType.INVENTORY_OPEN;
            Time.timeScale = 0f;
            canvasGroup.alpha = 1;
        }
        else
        {
            playerStatus.Status = PlayerStatusType.NONE;
            Time.timeScale = 1f;
            canvasGroup.alpha = 0;
        }
    }
}
