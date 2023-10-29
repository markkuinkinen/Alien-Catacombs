using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyScript : MonoBehaviour
{
    [SerializeField]
    private int currencyAmount;

    GameController gameController;
    PlayerController player;

    private bool playerInRange;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.GetComponent<Transform>().position - this.transform.position).normalized;
        transform.Translate(direction * 3f * Time.deltaTime);
    }

    public void SetCurrencyAmount(int amount)
    {
        currencyAmount = amount;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameController.addCurrency(currencyAmount);
            Destroy(this.gameObject);
        }

        if (collision.tag == "PlayerMagnet")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMagnet")
        {
            playerInRange = false;
        }
    }
}
