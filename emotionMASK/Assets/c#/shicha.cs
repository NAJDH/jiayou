using System.Collections.Generic;
using UnityEngine;

public class shicha : MonoBehaviour
{
    private GameObject Camera;
    [SerializeField]private float parallaxEffect;
    private float xPosition;
    private float length;
    private float scale;
    void Start()
    {
        Camera = GameObject.Find("Virtual Camera");
        xPosition = transform.position.x;
        scale = transform.localScale.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distanceToMove = Camera.transform.position.x * parallaxEffect;
        float distanceMoved = Camera.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y, transform.position.z);

        if(distanceMoved > xPosition + length) 
        {
            xPosition += length;
        }
        else if(distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
    }
}
