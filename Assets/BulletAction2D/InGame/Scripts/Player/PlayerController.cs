using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerAnimation playerAnimation;
    private Wepon wepon;
    private PlayerItemInventory playerItemInventory;

    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        wepon = GetComponentInChildren<Wepon>();
        playerItemInventory = GetComponent<PlayerItemInventory>();

    }

    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        playerMove.MoveDirection = moveInput.x;
        playerAnimation.animeRun(moveInput.x);

    }

    public void OnJumpStart()
    {
        playerMove.jumpStart();
        Debug.Log("jump start");
    }

    public void OnJumpEnd()
    {
        playerMove.jumpEnd();
        Debug.Log("jump end");
    }

    public void OnFire()
    {
        //Debug.Log("Fire");
        wepon.Attack(playerItemInventory.getActiveBullet());
    }

    public void OnCircle(InputValue inputValue)
    {
        //Debug.Log("Circle" + inputValue.Get<Vector2>());
        wepon.SetCircle(inputValue.Get<Vector2>());
    }

    public void OnItemGet()
    {
        Debug.Log("item get");
        playerItemInventory.setBulletData();
    }

    public void OnBulletChange()
    {
        playerItemInventory.changeActiveBulletIndex();
    }
}
