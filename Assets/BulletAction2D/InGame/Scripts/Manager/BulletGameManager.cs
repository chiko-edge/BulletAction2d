using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGameManager : MonoBehaviour
{
    private GameObject startPoint;
    private string startPointWord = "StartPoint";
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerInstance;


    private void Awake()
    {
        startPoint = GameObject.Find(startPointWord);

        if (isPlayerSpawn())
        {
            playerInstance = PlayerSpawn();
            playerInstance.GetComponent<PlayerController>().setStartPoint(startPoint.transform.position);

        }

    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// プレイヤーをスポーンさせる
    /// </summary>
    private GameObject PlayerSpawn()
    {
        return Instantiate(playerPrefab, startPoint.transform.position, Quaternion.identity);
    }

    private bool isPlayerSpawn()
    {
        if(playerPrefab == null || startPoint == null)
        {

            Debug.LogError($"プレイヤーをスポーンさせる情報が不足しています player{playerPrefab} : startPoint{startPoint}");
            return false;
        }


        return true;
    }

    /// <summary>
    /// プレイヤーをスタートポイント
    /// </summary>
    private void PlayerToStartPoint()
    {
        playerInstance.transform.position = startPoint.transform.position;
    }



}
