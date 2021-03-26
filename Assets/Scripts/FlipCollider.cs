using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCollider : MonoBehaviour
{
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0 )
        {
            //collider.transform.position = new Vector3(3,39, 0); 
            collider.transform.localPosition = new Vector2(1, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            //collider.transform.position = new Vector3(-3,39, 0);
            collider.transform.localPosition = new Vector2(-1, 0);
        }
        
    }

    private void FixedUpdate()
    {
    }
}
