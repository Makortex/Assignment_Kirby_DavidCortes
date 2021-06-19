using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class WaddleDee : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public int health;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 3.0f;
        }
        if (health <= 0)
        {
            health = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.flipX)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        //if (health<=0)
        //{
            //Destroy(gameObject);
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            //health--;
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    public void Death()
    {
        Destroy(gameObject.transform.parent.gameObject);

    }
}
