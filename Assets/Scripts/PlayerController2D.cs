using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isAttacking = false;

    bool isGrounded;

    bool isShooting = false;

    bool isFacingLeft = false;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform bulletSpawnPosL;

    [SerializeField]
    Transform bulletSpawnPosR;

    [SerializeField]
    GameObject attackHitBox;
    
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckL;

    [SerializeField]
    Transform groundCheckR;

    [SerializeField]
    float jumpHeight = 3.0f;

    [SerializeField] 
    float velocity = 3.0f;

    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //mapping commponenents
        rb2d = GetComponent<Rigidbody2D>();  //mapping commponenents
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackHitBox.SetActive(false);
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            if (!isGrounded)
            {
                animator.Play("jumpAttack");
                isAttacking = true;
                StartCoroutine(DoAttack());
            }
            else if (!isGrounded && (Input.GetKey("d") || Input.GetKey("right") || Input.GetKey("a") || Input.GetKey("left")))
            {
                animator.Play("jumpAttack");
                isAttacking = true;
                StartCoroutine(DoAttack());
            }
            else
            {
                //Debug.Log("Pressed primary button.");
                animator.Play("attack");
                isAttacking = true;

                StartCoroutine(DoAttack()); // coroutine to activate and deactivate after attack the hitbox. otherwise it can collide with objects
            }
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            isFacingLeft = false;
            if (isAttacking)
            {
                animator.Play("jumpAttack");
                isAttacking = true;
                StartCoroutine(DoAttack());
            }
            else if (isShooting)
            {
                animator.Play("shoot");
                isShooting = true;
                StartCoroutine(ResetShoot());

            }
            else
            {
                rb2d.velocity = new Vector2(velocity, rb2d.velocity.y);
                animator.Play("Player_run");
                spriteRenderer.flipX = false;

            }

        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            isFacingLeft = true;
            if (isAttacking)
            {
                animator.Play("jumpAttack");
                isAttacking = true;
                StartCoroutine(DoAttack());
            }
            else if (isShooting)
            {
                animator.Play("shoot");
                isShooting = true;
                StartCoroutine(ResetShoot());

            }
            else
            {
                rb2d.velocity = new Vector2(-velocity, rb2d.velocity.y);
                animator.Play("Player_run");
                spriteRenderer.flipX = true;

            }
        }
        else
        {
            if (!isAttacking && !isShooting)
            {
                animator.Play("idle");
            }

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            animator.Play("jump");
        }

        if (Input.GetMouseButton(1) && !isShooting)
        {
            //shoot
            animator.Play("shoot");
            isShooting = true;
            StartCoroutine(ResetShoot());

            //instantiate and shoot fire
            if (isFacingLeft)
            {
                GameObject Fire = Instantiate(bullet);
                Fire.GetComponent<BulletScript>().ShootFire(isFacingLeft);
                Fire.transform.position = bulletSpawnPosL.transform.position;

            }
            else
            {
                GameObject Fire = Instantiate(bullet);
                Fire.GetComponent<BulletScript>().ShootFire(isFacingLeft);
                Fire.transform.position = bulletSpawnPosR.transform.position;
            }
        }

    }

    IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(0.5f);
        isShooting = false;
        animator.Play("idle");
    }

    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.24f);
        attackHitBox.SetActive(false);
        isAttacking = false;
    }

    private void FixedUpdate() //because we use physics it is better to use fix update than update
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) 
            || Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))
            )
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            //if (!isAttacking)
            //{
            //    animator.Play("jump");
            //}
        }
   


    }

    void ResetAtack()
    {
        isAttacking = false;
    }
}

