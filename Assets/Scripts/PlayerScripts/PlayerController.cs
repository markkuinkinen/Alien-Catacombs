using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    UIController UIController;
    public Transform centreTransform;   // to control the rotation for the player sprite
    public GunController gunController;
    Rigidbody2D rb;
    Camera camera;
    public GameObject projectile;
    public GameObject projectileSpawnLoc;
    public Vector2 projectileDirection;
    
    //public GameObject gun;

    private bool isAlive;

    [SerializeField]
    private float moveSpeed = 15f;
    private float diagonalMoveSpeed = 15f * 0.7f;

    void Start()
    {
        UIController = FindObjectOfType<UIController>();
        rb = GetComponent<Rigidbody2D>();
        camera = GetComponent<Camera>();
        isAlive = true;
    }

    void Update()
    {
        if (isAlive && !UIController.isPaused)
        {
            Move();
            TrackMouse();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    private void TrackMouse()
    {
        //Debug.Log("tracking mouse");
        var camera = Camera.main;
        var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mouseDirection = ((Vector2)mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        centreTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        projectileDirection = ((Vector2)mousePos - (Vector2)transform.position);    //test zone

        /*if (Input.GetMouseButtonDown(0))
        {
            GameObject projectileClone = Instantiate(projectile, projectileSpawnLoc.GetComponent<Transform>().position, projectileSpawnLoc.GetComponent<Transform>().rotation);
            Vector2 direction = ((Vector2)mousePos - (Vector2)transform.position);//.normalized; //hella speedy when not normalised 
            projectileClone.GetComponent<ProjectileController>().SetDirection(direction);
        }*/

        if (Input.GetMouseButtonDown(0) && gunController.canShoot)
        {
            //Debug.Log("shooting");
            gunController.isShooting = true;
            //StartCoroutine(gunController.ShootingCooldown());
            //gunController.lastShotTime = Time.time;
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
            moveSpeed = diagonalMoveSpeed;
        }
        else
        {
            moveSpeed = 3f;
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))       // gets user key inputs and changes velocity accordingly
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
