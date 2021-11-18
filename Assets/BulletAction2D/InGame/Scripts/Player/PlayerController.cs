using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator = null;
    private Rigidbody2D rigidbody = null;
    private PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();

        playerMove.test();

    }

    public void OnMove(InputValue inputValue)
    {
        Debug.Log("move" + inputValue.Get<Vector2>());
    }

    public void OnJump()
    {
        Debug.Log("jump");
    }

    public void OnFire()
    {
        Debug.Log("Fire");
    }

    public void OnCircle(InputValue inputValue)
    {
        Debug.Log("Circle" + inputValue.Get<Vector2>());
    }
}
