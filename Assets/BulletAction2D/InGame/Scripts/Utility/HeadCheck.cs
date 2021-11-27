using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    private string groundTag = "Ground";

    private bool isHead = false;
    private bool isHeadEnter, isHeadStay, isHeadExit;

    //�ڒn�����Ԃ����\�b�h
    //��������̍X�V���ɌĂԕK�v������
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
