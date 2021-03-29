using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField]
    Object destructableRef;

    [SerializeField]
    Object partcileExplosionRef;

    [SerializeField]
    int health = 1;

    public static bool isDestroyed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon")|| collision.CompareTag("Bullet")) // find the collision parameter object by tag 
        {
            health--;

            

            if (health <= 0)
            {
                ExplodeThisGameObject();
                isDestroyed = true;
            }
        }
    }

    void ExplodeThisGameObject()
    {
        
        GameObject destructable = (GameObject)Instantiate(destructableRef);
        //GameObject particles = (GameObject)Instantiate(partcileExplosionRef);

        // mapping the loaded destructable object to x and y of previously destroyed barrel
        destructable.transform.position = transform.position;
        //particles.transform.position = transform.position;

        Destroy(gameObject);
    }

    
}
