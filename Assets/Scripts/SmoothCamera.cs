using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [Range(0,1)]
    public float smoothing;
    public Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
        {
            if(player.position != transform.position)
            {
                Vector3 playerPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);
            }
        }
    }
}
