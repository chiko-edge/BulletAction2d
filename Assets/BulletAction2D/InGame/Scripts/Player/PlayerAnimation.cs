using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    /// <summary>
    /// �ړ��̃A�j���[�V�����ƌ�����ݒ肷��
    /// 
    /// </summary>
    /// <param name="moveInputX"></param>
    public void animeRun(float moveInputX)
    {
        //0�ȊO�Ȃ�ړ��A�j���[�V����
        if(moveInputX != 0.0f)
        {
            transform.localScale = new Vector3(moveInputX, 1, 1);
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }


    }

}
