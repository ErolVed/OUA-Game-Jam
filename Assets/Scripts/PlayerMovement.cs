using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Karakterin hareket hızı
    [SerializeField] private float jumpForce = 10f; // Karakterin zıplama kuvveti

    [Header("Combat Settings")]
    [SerializeField] private int hitDamage = 10; // Vuruşta düşmana verilecek hasar
    [SerializeField] private Transform hitCheck = null; // Vuruşun yapılacağı alanın pozisyonu
    [SerializeField] private float hitCheckRadius = 0.5f; // Vuruşun yapılacağı alanın yarıçapı
    [SerializeField] private LayerMask enemyLayer; // Düşman katmanı

    private Rigidbody2D rb = null;
    private Animator anim = null;
    private bool isFacingRight = true; // Karakter sağa mı dönük?
    private bool isGrounded = false; //Karakter yerde mi?

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
        CheckGround();
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public void CheckGround()
    {
        float distance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, LayerMask.GetMask("Ground"));
        if(hit != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    } //Karakterin yerde olup olmadığını kontrol etme
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
