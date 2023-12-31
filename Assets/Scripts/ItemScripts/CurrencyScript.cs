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
    public bool magnetInRange;
    public float currencyMovespeed = 9f;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<PlayerController>();
        this.transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            MoveTowardsPlayer(player.currentMoveSpeed + 1f);
        }

        if (magnetInRange)
        {
            MoveTowardsPlayer(currencyMovespeed);
        }
    }

    private void MoveTowardsPlayer(float movespeed)
    {
        Vector3 direction = (player.GetComponent<Transform>().position - this.transform.position).normalized;
        transform.Translate(direction * movespeed * Time.deltaTime);
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
            magnetInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMagnet")
        {
            magnetInRange = false;
        }
    }

}
