using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    private string groundTag = "Ground";

    private bool isHead = false;
    private bool isHeadEnter, isHeadStay, isHeadExit;

    //接地判定を返すメソッド
    //物理判定の更新毎に呼ぶ必要がある
    public bool IsHead()
    {
        if (isHeadEnter || isHeadStay)
        {
            isHead = true;
        }
        else if (isHeadExit)
        {
            isHead = false;
        }

        isHeadEnter = false;
        isHeadStay = false;
        isHeadExit = false;
        return isHead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isHeadEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isHeadStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isHeadExit = true;
        }
    }
}
