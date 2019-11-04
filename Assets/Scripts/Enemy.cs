using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float distanceToPlayer = 2, delay = 2.0f, speed = 2;
    public float enemyHealth = 3;
    public bool isTouched;
    public GameObject orbEnnemy, shootPoint;

    private bool canShoot;
    

    private void Start() {
        canShoot = true;
        isTouched = false;
    }

    private void FixedUpdate() {
        Move();
        Shoot();
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        if(isTouched)
        {
            StartCoroutine(StopVelocityCo());
        }
    }

    private void Move()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (distanceToPlayer < Vector3.Distance(transform.position,
                GameObject.FindGameObjectWithTag("Player").transform.position))
            {
                transform.position = Vector3.MoveTowards(transform.position,
                                        GameObject.FindGameObjectWithTag("Player").transform.position, speed / 100);  
                
            }

        Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        
    }

    private void Shoot()
    {
        if(canShoot)
        {
            StartCoroutine(ShootCo(delay));
            Instantiate(orbEnnemy, shootPoint.transform.position, gameObject.transform.rotation);
        }
    }

    IEnumerator ShootCo(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(this.delay);
        canShoot = true;
    }

    IEnumerator StopVelocityCo()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isTouched = false;
    }

}
