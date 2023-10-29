using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    public GameObject eggCurrency;
    public GameObject eggEvolvedSpawn;
    GameController gameController;

    private int Health = 40;

    float timer = 0f;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        EvolveEgg();
    }

    void EvolveEgg()
    {
        timer += Time.deltaTime;

        if (timer > 5f)
        {
            Instantiate(eggEvolvedSpawn, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Health -= collision.GetComponent<ProjectileController>().GetProjectileDamage();
            if (Health <= 0)
            {
                GameObject eggCurrencyDrop = Instantiate(eggCurrency, this.transform.position, this.transform.rotation);
                eggCurrencyDrop.GetComponent<CurrencyScript>().SetCurrencyAmount(20);
                gameController.giveExp(50);
                Destroy(this.gameObject);
            }
        }
    }
}
