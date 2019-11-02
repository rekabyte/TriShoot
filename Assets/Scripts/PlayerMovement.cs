using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, shootDelay, dashDelay, dashForce, dashTime;

    public GameObject orb;
    public GameObject shotPoint, trailPoint;

    private Rigidbody2D rb;
    private bool canShoot, canDash;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canShoot = true;
        canDash = true;
        trailPoint.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        LookAt();
        Move();
        Shoot();
        Dash();
    }

    void LookAt()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Move()
    {
        Vector3 kbInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position+= kbInput * speed * Time.deltaTime;
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(DashCo());
            rb.AddForce(transform.up * dashForce, ForceMode2D.Impulse);
            StartCoroutine(StopCo(dashTime));
        }
    }

    void Shoot()
    {
        if(canShoot)
        {
            if(Input.GetButton("Fire1"))
            {
                StartCoroutine(ShootCo());
                Instantiate(orb, shotPoint.transform.position, this.gameObject.transform.rotation);
            }
        }
    }

    IEnumerator ShootCo()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    IEnumerator DashCo()
    {
        canDash = false;
        trailPoint.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(dashDelay);
        
        canDash = true;
    }

    IEnumerator StopCo(float dashTime)
    {
        yield return new WaitForSeconds(this.dashTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2.0f);
        trailPoint.gameObject.SetActive(false);
    }


}
