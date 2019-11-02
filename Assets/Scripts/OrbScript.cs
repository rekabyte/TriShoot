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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Tile")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestructCo()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
