using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    UIController UIController;
    GameController GameController;
    Rigidbody2D rb;
    PlayerController player;
    public GameObject eyeCurrency;
    private float movespeed = 3.5f;
    //public Transform centreOfEnemy;

    //give hp to lower

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        UIController = FindObjectOfType<UIController>();
        GameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.isPaused)
        {
            FollowPlayer();
        }

    }


    void FollowPlayer()
    {
        Vector3 direction = (player.GetComponent<Transform>().position - this.transform.position).normalized;
        transform.Translate(direction * movespeed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            GameObject eyeDroppedCurrency = Instantiate(eyeCurrency, this.transform.position, this.transform.rotation);
            eyeDroppedCurrency.GetComponent<CurrencyScript>().SetCurrencyAmount(15);
            GameController.giveExp(100);
            Destroy(this.gameObject);
        }
    }
}
