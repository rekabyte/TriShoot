using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public float orbForce;

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
        } else if (other.gameObject.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.enemyHealth -= 1;
            Destroy(gameObject);
        }
    }

    IEnumerator DestructCo()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
