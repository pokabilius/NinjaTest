using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    GameObject attackHitBox;

    [SerializeField]
    float chaseDistance = 10.0f;

    [SerializeField]
    float enemySpeed = 5.0f;

    Rigidbody2D rb2d;
    Animator animator;

    bool isAttacking;

    bool isInAttackRange;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            Chase();
        }
    }
    void Chase()
    {

        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(enemySpeed, 0);
            transform.localScale = new Vector2(0.4f, 0.4f);
            animator.Play("ork_walk");
        }
        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-enemySpeed, 0);
            transform.localScale = new Vector2(-0.4f, 0.4f);
            animator.Play("ork_walk");
        }


    }

    

}





//       if (distance < chaseDistance && !isAttacking && (!DieScript.isDead || !DamageScript.isDestroyed))
//       {
//           // attack to player
//           Chase();
//           if (distance < chaseDistance)
//           {
//               Chase();
//               if (isInAttackRange && (!DieScript.isDead || !DamageScript.isDestroyed))
//               {
//                   StartCoroutine(DoAttack());
//               }
//           }
//          
//          
//       }
//       else //if (distance > chaseDistance && !DieScript.isDead && !isInAttackRange)
//       {
//           StopChase();
//       }
//    
//   }
//
//  
//   IEnumerator DoAttack()
//   {
//       animator.Play("ork_attack");
//       attackHitBox.SetActive(true);
//       yield return new WaitForSeconds(0.5f);
//       attackHitBox.SetActive(false);
//       isAttacking = false;
//   }
//
//   void StopChase()
//   {
//       rb2d.velocity = new Vector2(0, 0);
//       animator.Play("ork_idle");
//   }
//
//   void Chase()
//   {
//      if (transform.position.x < player.position.x)
//       {
//           rb2d.velocity = new Vector2(enemySpeed, 0);
//           transform.localScale = new Vector2(0.4f, 0.4f);
//           animator.Play("ork_walk");
//       }
//      else if (transform.position.x > player.position.x)
//       {
//           rb2d.velocity = new Vector2(-enemySpeed, 0);
//           transform.localScale = new Vector2(-0.4f, 0.4f);
//           animator.Play("ork_walk");
//       }       
//   }
//  
//   private void OnCollisionStay2D(Collision2D collision)
//   {
//       if (collision.gameObject.tag == "DestructableObject" || collision.gameObject.tag == "Player")
//       {
//           Debug.Log("collision");
//           isInAttackRange = true;
//       }
//   }
//
//
//  
//

