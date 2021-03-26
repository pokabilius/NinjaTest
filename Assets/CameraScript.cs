using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffset;

    [SerializeField]
    Vector2 posistionOffset;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //camera start position
        Vector3 startPosition = transform.position;

        //players current position
        Vector3 endPosition = player.transform.position;

        endPosition.x += posistionOffset.x;
        endPosition.y += posistionOffset.y;
        endPosition.z = -10;

        //lerp
        transform.position = Vector3.Lerp(startPosition, endPosition, timeOffset * Time.deltaTime);

        //Smooth dampening
        //transform.position = Vector3.SmoothDamp(startPosition, endPosition, ref velocity, timeOffset);
    
    }
   
}
