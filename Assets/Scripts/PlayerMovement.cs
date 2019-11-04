using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, shootDelay, dashDelay, dashForce, dashTime;
    float timeLeft;

    public GameObject orb, shotPoint, trailPoint, dashPoint;
    public GameObject youDied, restartButton;

    public Text healthText;

    public Slider slider;

    public Health health;

    private Rigidbody2D rb;
    private bool canShoot, canDash;
    
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canShoot = true;
        canDash = true;
        timeLeft = 0;
        trailPoint.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        LookAt();
        Move();
        Shoot();
        Dash();
    }

    private void Update() {

        healthText.text = "HP : " + health.healthValue;

        timeLeft += Time.deltaTime;
        slider.value = timeLeft / dashDelay;

        

        if(health.healthValue <= 0)
        {
            youDied.SetActive(true);
            restartButton.SetActive(true);
            Destroy(gameObject);
        }  
    }

    private void LookAt()
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
            timeLeft = 0;
            dashPoint.gameObject.SetActive(true);
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().isTouched = true;
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
        dashPoint.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        trailPoint.gameObject.SetActive(false);
        
    }



}
