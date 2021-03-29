using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    int damage;
    
    public void ShootFire(bool isFacingLeft)
    {
        
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        
        if (isFacingLeft)
        {
            rb2d.velocity = new Vector2(-speed, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            rb2d.velocity = new Vector2(speed, 0);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DestructubleObject")
        {
            
            Destroy(gameObject);

        }
    }
}
