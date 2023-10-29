using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolvedEggScript : MonoBehaviour
{
    public GameObject EvolvedEggCurrency;
    GameController gameController;


    private int Health;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Health -= collision.GetComponent<ProjectileController>().GetProjectileDamage();

            if (Health <= 0)
            {
                //EvolvedEggCurrency.GetComponent<CurrencyScript>().SetCurrencyAmount(20);
                gameController.giveExp(50);
                Destroy(this.gameObject);
            }
        }
    }
}
