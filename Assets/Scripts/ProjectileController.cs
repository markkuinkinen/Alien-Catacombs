using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    UIController UIController;
    GunController gunController;
    Rigidbody2D rb;
    private Vector2 direction;
    private float projectileSpeed = 5f;

    //give numerical damage
    //projectiles have health
    //player sets type when instantiating

    private int projectileHealth;   //if bullets go through enemies
    
    //rifle: 10, laser: 20, rocket: 20
    private float projectileDamage = 10;
    private string ammoType;
    public GameObject rocketExplosion;
    //public float projectileDamageMultiplier = 1;

    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        UIController = FindObjectOfType<UIController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public float GetProjectileDamage()
    {
        return projectileDamage;
    }

    public void SetAmmoType(string type)
    {
        ammoType = type;
    }

    public void setProjectileHealth(int Health)
    {
        projectileHealth = Health;
    }

    public void setProjectileSpeed(float Speed)
    {
        projectileSpeed = Speed;
    }

    public void setProjectileDamage(float Damage)
    {
        projectileDamage = Damage;
    }

    public void SetDirection(Vector2 direction)     //sets the direction and angle/rotation of the projectile
    {
        this.direction = direction;

        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }


    void Update()
    {
        if (!UIController.isPaused)
        {
            rb.velocity = (direction * projectileSpeed).normalized * 10f;

        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        
        if (collision.tag == "Enemy")
        {
            projectileHealth -= 10;

            if (projectileHealth <= 0)
            {
                if (ammoType == "Rocket")
                {
                    this.GetComponent<CircleCollider2D>().enabled = true;
                    this.setProjectileSpeed(0f);
                    this.GetComponent<SpriteRenderer>().enabled = false;
                    rocketExplosion.SetActive(true);
                    Invoke("DestroyProjectile", 0.3f);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
