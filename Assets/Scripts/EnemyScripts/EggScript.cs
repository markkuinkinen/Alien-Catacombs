using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    public GameObject eggCurrency;
    public GameObject eggEvolvedSpawn;
    GameController gameController;
    UIController uiController;
    public List<GameObject> blood;

    private float Health = 40;

    float timer = 0f;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (!uiController.isPaused)
        {
            if (!uiController.enemyIsPaused)
            {
                EvolveEgg();
            }
        }
    }

    void EvolveEgg()
    {
        timer += Time.deltaTime;

        if (timer > 10f)
        {
            Instantiate(eggEvolvedSpawn, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void dropBlood()
    {
        Quaternion bloodRotation = Quaternion.Euler(0f, 270f, 90f);
        Instantiate(blood[Random.Range(0, 7)], this.transform.position, bloodRotation);
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
                dropBlood();
                Destroy(this.gameObject);
            }
        }
    }
}
