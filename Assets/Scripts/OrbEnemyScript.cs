using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbEnemyScript : MonoBehaviour
{
    public float orbForce = 2;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.up * orbForce);
        StartCoroutine(DestructCo());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Tile")
        {
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Player")
        {
            PlayerMovement p = other.gameObject.GetComponent<PlayerMovement>();
            p.health.healthValue -= 1;
            Destroy(gameObject);
        }
    }

    IEnumerator DestructCo()
    {
        yield return new WaitForSeconds(8.0f);
        Destroy(this.gameObject);
    }
}
