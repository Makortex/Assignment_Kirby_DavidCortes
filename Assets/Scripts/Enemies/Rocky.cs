using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

public class Rocky : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer RockySprite;

    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Jump", 1f, 3f);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        RockySprite = GetComponent<SpriteRenderer>();

        if (jumpForce <= 0)
        {
            jumpForce = 300;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }
        
    }
    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce);
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        anim.SetBool("isGrounded", isGrounded);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                GameManager.instance.score++;
            }
        }
    }
}
