using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    PlayerInputAction inputAction;
    Rigidbody2D rigid2d;

    [Header("점프")]
    /// <summary>
    /// 기본 점프 크기
    /// </summary>
    public float jumpForce = 5.0f;

    /// <summary>
    /// 발판을 밟았을 때의 점프크기
    /// </summary>
    public float powerJump = 10.0f;

    [Header("이동속도")]
    /// <summary>
    /// 기본 이동속도
    /// </summary>
    public float moveSpeed = 7.0f;

    /// <summary>
    /// 부스트존에 닿으면 속도가 올라감
    /// </summary>
    public float boostSpeed = 10.0f;

    [Header("캔버스")]
    public Canvas gameover;
    public Canvas clear;

    private bool isGround = true;
    private bool isPowerJump = true;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        rigid2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * Vector3.right * moveSpeed);
    }

    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputAction.Player.Jump.performed -= Jump;
        inputAction.Player.Disable();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if ( context.performed && isGround)
        { 
            rigid2d.velocity = new Vector2(rigid2d.velocity.x, jumpForce);
            isGround = false; 
        }

    }

    private void AutoJump()
    {
        if (isGround)
        {
            if (isPowerJump)
            {
                rigid2d.velocity = new Vector2(rigid2d.velocity.x, powerJump);
                isPowerJump = false;
            }
            else
            {
                rigid2d.velocity = new Vector2(rigid2d.velocity.x, jumpForce);
            }
            isGround = false;
        }
    }

    private void Boost()
    {
        Vector2 newSpeed = rigid2d.velocity;
        newSpeed.x = moveSpeed * boostSpeed;

        rigid2d.velocity = newSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGround)
        {
            isGround = true;
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            Time.timeScale = 0;
            if (gameover != null)
            {
                gameover.gameObject.SetActive(true);
            }
        }

        if(collision.gameObject.CompareTag("End"))
        {
            Time.timeScale = 0;
            if (clear != null)
            {
                clear.gameObject.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("PowerJump"))
        {
            AutoJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boost"))
        {
            Boost();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
