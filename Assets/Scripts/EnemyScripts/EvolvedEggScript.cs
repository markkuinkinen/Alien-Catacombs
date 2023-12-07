using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolvedEggScript : MonoBehaviour
{
    public GameObject EvolvedEggCurrency;
    GameController gameController;
    UIController uiController;
    PlayerController player;
    public List<GameObject> blood;
    private float movespeed = 9.5f;

    private float Health = 70;

    [SerializeField]
    private float timer;

    private Vector3 directionToMove;
    private Vector3 directionOfPlayer;

    public Transform movementTransform;
    public Transform centreOfEnemy;

    float angleToLook;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UIController>();
        directionToMove = getDirection();//directionOfPlayer;

        rotate();
    }


    void Update()
    {

        if (!uiController.isPaused)
        {
            if (!uiController.enemyIsPaused)
            {
                directionOfPlayer = (player.GetComponent<Transform>().position - this.transform.position).normalized;

                timer += Time.deltaTime;
                if (timer < 3f)
                {
                    movementTransform.Translate(directionToMove * movespeed * Time.deltaTime);
                }
                else
                {
                    directionToMove = (player.GetComponent<Transform>().position - transform.position).normalized;
                    rotate();
                    //float angle = Mathf.Atan2(directionToMove.y, directionToMove.x) * Mathf.Rad2Deg;
                    //centreOfEnemy.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

                    timer = 0f;
                }
            }
        }
    }


    Vector3 getDirection()
    {
        return (player.GetComponent<Transform>().position - this.transform.position).normalized;
    }


    void rotate()
    {
        angleToLook = Mathf.Atan2(directionToMove.y, directionToMove.x) * Mathf.Rad2Deg;
        centreOfEnemy.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(directionToMove.y, directionToMove.x) * Mathf.Rad2Deg - 90f));
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.GetComponent<Transform>().position - this.transform.position).normalized;
        transform.Translate(direction * movespeed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
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
            Debug.Log("hit the spider");

            if (Health <= 0)
            {
                EvolvedEggCurrency.GetComponent<CurrencyScript>().SetCurrencyAmount(30);
                gameController.giveExp(50);
                dropBlood();
                Destroy(this.gameObject);
            }
        }
    }
}
