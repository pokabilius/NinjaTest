using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    [SerializeField]
    Animator dieAnimation;

    //[SerializeField]
    //Object partcileExplosionRef;

    [SerializeField]
    int health = 4;

    public static bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        dieAnimation = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") || collision.CompareTag("Bullet")) // find the collision parameter object by tag 
        {
            health--;



            if (health <= 0)
            {
                isDead = true;
                dieAnimation.Play("ork_die");
                Invoke("DestroyObject", 1.3f);

            }
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
