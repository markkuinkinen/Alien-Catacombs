using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    UIController UIController;
    Rigidbody2D rb;
    private Vector2 direction;
    private float projectileSpeed = 5f;

    //give numerical damage


    void Start()
    {
        //trans = GetComponent<Transform>();
        UIController = FindObjectOfType<UIController>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void SetDirection(Vector2 direction)     //sets the direction and angle of the projectile
    {
        this.direction = direction;

        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private void OnDestroy()
    {
        //destroy if +-20 from player 
    }

    void Update()
    {
        if (!UIController.isPaused)
        {
            rb.velocity = direction * projectileSpeed;

        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
