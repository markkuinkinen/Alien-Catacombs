using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    UIController UIController;
    GameController gameController;
    public Transform centreTransform;   // to control the rotation for the player sprite
    public GunController gunController;
    Rigidbody2D rb;
    Camera camera;
    public GameObject projectile;
    public GameObject projectileSpawnLoc;
    public Vector2 projectileDirection;
    
    //public GameObject gun;

    public bool isAlive;

    [SerializeField]
    private float baseMoveSpeed = 3f;
    public float currentMoveSpeed;
    public float moveSpeedMultiplier = 1f;

    private float dodgeMultiplier = 1f;
    public float dodgeDuration = 0.3f;
    [SerializeField]
    private bool isDodging;

    private float timer;
    public bool isInvulnerable;
    public BoxCollider2D damageHitBox;
    public SpriteRenderer[] playerSprites;
    public SpriteRenderer playerChevronSprite;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        UIController = FindObjectOfType<UIController>();
        rb = GetComponent<Rigidbody2D>();
        camera = GetComponent<Camera>();
        isAlive = true;
        isInvulnerable = false;
        currentMoveSpeed = baseMoveSpeed * moveSpeedMultiplier;
        //diagonalMoveSpeed = (currentMoveSpeed * 0.7f) * moveSpeedMultiplier * dodgeMultiplier;
    }

    void Update()
    {
        //diagonalMoveSpeed = (currentMoveSpeed * 0.7f) * moveSpeedMultiplier * dodgeMultiplier;

        if (isAlive && !UIController.isPaused)
        {
            Move();
            TrackMouse();
            if (gameController.returnHasDash())
            {
                SideStep();
            }

            if (isDodging)
            {
                StartCoroutine(isSideStepping());
            }
            else
            {
                StopCoroutine(isSideStepping());
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    IEnumerator isSideStepping()
    {
        dodgeMultiplier = 2.5f;

        yield return new WaitForSeconds(dodgeDuration);

        isDodging = false;
        dodgeMultiplier = 1f;
    }

    IEnumerator takingDamage()
    {
        isInvulnerable = true;
        var originalHitBoxSize = damageHitBox.size;
        damageHitBox.size = new Vector2(0f, 0f);
        var originalColor = playerSprites[0].color;
        var originalChevronColor = playerChevronSprite.color;
        for (int i = 0; i < playerSprites.Length; i++)
        {
            playerSprites[i].color = new Color(1, 1, 1, 0.35f);
        }
        playerChevronSprite.color = new Color(1, 0, 0, 0.35f);

        yield return new WaitForSeconds(2f);
        isInvulnerable = false;
        damageHitBox.size = originalHitBoxSize;
        for (int i = 0; i < playerSprites.Length - 1; i++)
        {
            playerSprites[i].color = originalColor;
        }
        playerChevronSprite.color = originalChevronColor;
    }

    void SideStep()
    {
        if (Input.GetKey(KeyCode.Space) && !isDodging)
        {
            isDodging = true;
        }
    }


    private void TrackMouse()
    {

        var camera = Camera.main;
        var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mouseDirection = ((Vector2)mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        centreTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        projectileDirection = ((Vector2)mousePos - (Vector2)transform.position);    //test zone


        if (Input.GetMouseButtonDown(0) && gunController.canShoot)
        {
            gunController.isShooting = true;
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            gunController.isShooting = false;
        }
    }

    private void Move()
    {
        if ((rb.velocity.x > 0 || rb.velocity.x < 0) && (rb.velocity.y > 0 || rb.velocity.y < 0))   // limits diagonal speed direction and reverts it 
        {
            //currentMoveSpeed = diagonalMoveSpeed * dodgeMultiplier;
            currentMoveSpeed = (baseMoveSpeed * gameController.returnMovespeedMultiplier() * dodgeMultiplier) * 0.75f;
        }
        else
        {
            currentMoveSpeed = baseMoveSpeed * gameController.returnMovespeedMultiplier() * dodgeMultiplier;
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))       // gets user key inputs and changes velocity accordingly
        {
            rb.velocity = new Vector2(rb.velocity.x, currentMoveSpeed * gameController.returnMovespeedMultiplier());
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -currentMoveSpeed * gameController.returnMovespeedMultiplier());
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-currentMoveSpeed * gameController.returnMovespeedMultiplier(), rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(currentMoveSpeed * gameController.returnMovespeedMultiplier(), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * gameController.returnMovespeedMultiplier());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isInvulnerable)
        {
            gameController.currentHp -= 20;
            StartCoroutine(takingDamage());
        }

        // Gives full HP
        if (collision.tag == "Beer")
        {
            gameController.currentHp = gameController.maxPlayerHealth;
        }

        // Gives half hp
        if (collision.tag == "Meat")
        {
            if (gameController.currentHp + (gameController.maxPlayerHealth / 2) <= gameController.maxPlayerHealth)
            {
                gameController.currentHp += gameController.maxPlayerHealth / 2;
                Destroy(collision.gameObject);
            }
            else
            {
                gameController.currentHp = gameController.maxPlayerHealth;
                Destroy(collision.gameObject);
            }
        }
    }
}
