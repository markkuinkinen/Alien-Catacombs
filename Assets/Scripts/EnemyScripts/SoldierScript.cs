using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    UIController UIController;
    GameController GameController;
    Rigidbody2D rb;
    PlayerController player;
    public Transform centreOfEnemy;
    public GameObject soldierCurrency;
    public List<GameObject> blood;

    private float movespeed = 2f;
    [SerializeField]
    private float Health = 40;

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
            if (!UIController.enemyIsPaused)
            {
                FollowPlayer();
            }
        }

    }


    void FollowPlayer()
    {
        Vector3 direction = (player.GetComponent<Transform>().position - this.transform.position).normalized;
        transform.Translate(direction * movespeed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        centreOfEnemy.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void dropBlood()
    {
        Quaternion bloodRotation = Quaternion.Euler(0f, 270f, 90f);
        Instantiate(blood[Random.Range(0, 7)], this.transform.position, bloodRotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            Health -= other.GetComponent<ProjectileController>().GetProjectileDamage();
            Debug.Log("hit the soldier");

            if (Health <= 0)
            {
                GameObject currencyDrop = Instantiate(soldierCurrency, this.transform.position, this.transform.rotation);
                currencyDrop.GetComponent<CurrencyScript>().SetCurrencyAmount(20);
                GameController.giveExp(200);
                dropBlood();
                Destroy(this.gameObject);
            }
        }
    }
}
