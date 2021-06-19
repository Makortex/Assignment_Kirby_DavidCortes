using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer kirbySprite;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    AudioSource pickupAudioSource;
    AudioSource jumpAudioSource;

    public AudioClip jumpSFX;
    public AudioMixerGroup audioMixer;


    public bool canFly;

    bool coroutineRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        kirbySprite = GetComponent<SpriteRenderer>();
        pickupAudioSource = GetComponent<AudioSource>();


        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 350;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please assign a ground check object");
        }

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            if (!jumpAudioSource)
            {
                jumpAudioSource = gameObject.AddComponent<AudioSource>();
                jumpAudioSource.clip = jumpSFX;
                jumpAudioSource.outputAudioMixerGroup = audioMixer;
                jumpAudioSource.loop = false;
            }

            jumpAudioSource.Play();

        }
        else if (canFly && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            if (!jumpAudioSource)
            {
                jumpAudioSource = gameObject.AddComponent<AudioSource>();
                jumpAudioSource.clip = jumpSFX;
                jumpAudioSource.outputAudioMixerGroup = audioMixer;
                jumpAudioSource.loop = false;
            }

            jumpAudioSource.Play();

        }

        Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);

        if (kirbySprite.flipX && horizontalInput > 0 || !kirbySprite.flipX && horizontalInput < 0)
            kirbySprite.flipX = !kirbySprite.flipX;

    }
    public void Fly()
    {
        if (!coroutineRunning)
        {
            StartCoroutine("JumpForceChange");
        }
        else
        {
            StopCoroutine("JumpForceChange");
            StartCoroutine("JumpForceChange");
        }
    }

    IEnumerator JumpForceChange()
    {
        coroutineRunning = true;
        canFly = true;
        yield return new WaitForSeconds(10.0f);
        canFly = false;
        coroutineRunning = false;
    }
    public void CollectibleSound(AudioClip pickupAudio)
    {
        pickupAudioSource.clip = pickupAudio;
        pickupAudioSource.Play();
    }
}