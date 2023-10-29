using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolvedEggScript : MonoBehaviour
{
    public GameObject EvolvedEggCurrency;
    GameController gameController;
    PlayerController player;
    private float movespeed = 6.5f;

    private int Health = 60;

    [SerializeField]
    private float timer;

    private Vector3 directionToMove;

    private Vector3 directionOfPlayer;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();

        directionToMove = getDirection();//directionOfPlayer;
    }


    void Update()
    {
        directionOfPlayer = (player.GetComponent<Transform>().position - this.transform.position).normalized;

        timer += Time.deltaTime;
        if (timer < 4f)
        {
            transform.Translate(directionToMove * movespeed * Time.deltaTime);
        }
        else
        {
            directionToMove = (player.GetComponent<Transform>().position - transform.position).normalized;

            //float angle = Mathf.Atan2(directionToMove.y, directionToMove.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            timer = 0f;
        }
    }


    Vector3 getDirection()
    {
        return (player.GetComponent<Transform>().position - this.transform.position).normalized;
    }


    void rotate()
    {
        float angle = Mathf.Atan2(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.GetComponent<Transform>().position - this.transform.position).normalized;
        transform.Translate(direction * movespeed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Health -= collision.GetComponent<ProjectileController>().GetProjectileDamage();

            if (Health <= 0)
            {
                EvolvedEggCurrency.GetComponent<CurrencyScript>().SetCurrencyAmount(30);
                gameController.giveExp(50);
                Destroy(this.gameObject);
            }
        }
    }
}
