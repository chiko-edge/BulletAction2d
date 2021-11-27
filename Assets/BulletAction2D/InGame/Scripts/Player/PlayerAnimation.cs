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
    /// 移動のアニメーションと向きを設定する
    /// 
    /// </summary>
    /// <param name="moveInputX"></param>
    public void animeRun(float moveInputX)
    {
        //0以外なら移動アニメーション
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
