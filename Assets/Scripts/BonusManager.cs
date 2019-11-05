using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{   
    public float duration;

    private bool isBomb=false, isSpeed=false, isRate=false, isHeart=false;

    // Start is called before the first frame update
    void Awake()
    {
        if(this.gameObject.name == "speedUpgrade(Clone)")
        {
            isSpeed = true;
        } else if (this.gameObject.name == "rateUpgrade(Clone)")
        {
            isRate = true;
        } else if (this.gameObject.name == "bomb(Clone)")
        {
            isBomb = true;
        } else if (this.gameObject.name == "heart(Clone)")
        {
            isHeart = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(isSpeed)
            {
                other.gameObject.GetComponent<PlayerMovement>().speed *= 2;
                other.gameObject.GetComponent<PlayerMovement>().StartCoroutine(
                    other.gameObject.GetComponent<PlayerMovement>().SpeedCo(duration));
                
                Destroy(gameObject);
            } else if (isRate)
            {
                other.gameObject.GetComponent<PlayerMovement>().shootDelay /= 2;
                other.gameObject.GetComponent<PlayerMovement>().StartCoroutine(
                    other.gameObject.GetComponent<PlayerMovement>().RateCo(duration));

                Destroy(gameObject);
            } else if (isHeart)
            {
                other.gameObject.GetComponent<PlayerMovement>().health.healthValue++;

                Destroy(gameObject);
            }


        }
    }
}
