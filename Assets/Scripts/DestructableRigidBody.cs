using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRigidBody : MonoBehaviour
{
    [SerializeField]
    float torque;

    [SerializeField]
    Vector2 direction;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        float randTorque = UnityEngine.Random.Range(torque - 100, torque + 100);
        float randDirectionX = UnityEngine.Random.Range(direction.x - 100, direction.y + 100);
        float randDirectionY = UnityEngine.Random.Range(direction.y , direction.y + 200);

        randDirectionX = direction.x;
        randDirectionY = direction.y;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(direction);
        rb2d.AddTorque(randTorque);

        Invoke("DestroySelf", Random.Range(1.0f, 4.0f));
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
