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
    private PlayerStatus playerStatus;


    private Vector2 moveInput;

    private Vector2 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        wepon = GetComponentInChildren<Wepon>();
        playerItemInventory = GetComponent<PlayerItemInventory>();
        playerStatus = GetComponent<PlayerStatus>();

    }

    public void OnMove(InputValue inputValue)
    {
        if (playerStatus.isMove())
        {
            moveInput = inputValue.Get<Vector2>();
            playerMove.MoveDirection = moveInput.x;
            playerAnimation.animeRun(moveInput.x);
        }


    }

    public void OnJumpStart()
    {
        if (playerStatus.isJump())
        {
            playerMove.jumpStart();
        }

    }

    public void OnJumpEnd()
    {
        if (playerStatus.isJump())
        {
            playerMove.jumpEnd();
        }

    }

    public void OnFire()
    {
        if (playerStatus.isFire())
        {
            wepon.Attack(playerItemInventory.getActiveBullet());
        }

    }

    public void OnCircle(InputValue inputValue)
    {
        if (playerStatus.isCircle())
        {
            wepon.SetCircle(inputValue.Get<Vector2>());
        }
        else if (playerStatus.isInventoryOpen())
        {
            playerItemInventory.MousePosition = inputValue.Get<Vector2>();
        }

    }

    public void OnItemGet()
    {
        if (playerStatus.isItemGet())
        {
            playerItemInventory.setBulletData();
        }

    }

    public void OnBulletChange()
    {
        if (playerStatus.isBulletChange())
        {
            playerItemInventory.changeActiveBulletIndex();
        }
    }

    public void OnInventoryOpen()
    {
        if (playerStatus.isInventoryOpen())
        {
            playerItemInventory.InventoryView(playerStatus);
        }

    }

    public void setStartPoint(Vector3 targetPoint)
    {
        startPoint = targetPoint;
    }

    public void RestertPlayer()
    {
        transform.position = startPoint;
    }
}
