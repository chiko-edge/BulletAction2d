using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator = null;
    private PlayerMove playerMove;

    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();

    }

    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        Debug.Log("move" + moveInput);
        playerMove.MoveDirection = moveInput.x;

    }

    public void OnJump()
    {
        //Debug.Log("jump");
    }

    public void OnFire()
    {
        //Debug.Log("Fire");
    }

    public void OnCircle(InputValue inputValue)
    {
        //Debug.Log("Circle" + inputValue.Get<Vector2>());
    }
}
