using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    Transform rayCast;

    [SerializeField]
    LayerMask raycastMask;

    [SerializeField]
    float rayCastLength;

    [SerializeField]
    float attackDistance; // min distance for attack

    [SerializeField]
    float movespeed;

    [SerializeField]
    float timer; //cooldown time btw attacks

    [SerializeField]
    Transform leftLimit;

    [SerializeField]
    Transform rightLimit;

    RaycastHit2D hit;
    Transform target;
    Animator animator;
    float distance;
    bool attackMode;
    bool inRange;
    bool cooling; //check if enemy is cooling before attack
    float intTimer;

    private void Awake()
    {
        SelectTarget();
        intTimer = timer; //stores initial value of timer
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inRange && animator.GetCurrentAnimatorStateInfo(0).IsName("ork_attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            //Debug.Log("inrange");
            hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        // when player is detected
        if (hit.collider != null)
        {
            Debug.Log("enemy should move");
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            //Debug.Log("collider null");
            inRange = false;
        }

        if (!inRange)
        {
           
            StopAttack();
        }

        void RaycastDebugger()
        {
            if (distance > attackDistance)
            {
                Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);

            }
            else if (attackDistance > distance)
            {
                Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
            }
        }
    }

    void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
           
            StopAttack();
        }
        else if (attackDistance >= distance && !cooling)
        {
            Attack();
        }

        if (cooling)
        {
            CoolDown();
            animator.SetBool("Attack", false);
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    void Attack()
    {
        timer = intTimer;//reset timer when entering attack range
        attackMode = true; // to check if enemy can attack or not

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void Move()
    {
        animator.SetBool("canWalk", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ork_attack"))// check if is not attacking
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movespeed * Time.deltaTime);//other method to move characters
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag =="Player")
        {
            //Debug.Log("inrange");
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
}
