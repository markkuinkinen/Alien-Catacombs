using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    UIController UIController;
    GunController gunController;
    GameController GameController;
    Rigidbody2D rb;
    PlayerController player;
    public Transform centreOfEnemy;

    //give hp to lower
    private float movespeed = 2f;
    private float health = 20;

    public bool canMove; //delete
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        UIController = FindObjectOfType<UIController>();
        GameController = FindObjectOfType<GameController>();
        gunController = FindObjectOfType<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.isPaused && canMove) //delete
        {
            FollowPlayer();
        }

    }
    

    void FollowPlayer()
    {
        Vector3 direction = (player.GetComponent<Transform>().position - this.transform.position).normalized;
        transform.Translate(direction * movespeed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        centreOfEnemy.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            health -= gunController.getAmmoDamage();
            if (health == 0)
            {
                GameController.giveExp(100);
                Destroy(this.gameObject);  //change
            }

        }
    }
}
