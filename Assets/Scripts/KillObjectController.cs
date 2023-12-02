using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectController : MonoBehaviour
{
    UIController uiController;
    SoundController soundController;
    public GameObject projectilePrefab;
    public GameObject turret;
    public Transform projectileSpawnLocation;
    private bool isShooting;
    private float rotationSpeed = 160f;

    public float timer;
    public float lifeTimer;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        uiController = FindObjectOfType<UIController>();
        timer = 0f;
        lifeTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            if (!uiController.isPaused)
            {
                lifeTimer += Time.deltaTime;
                if (lifeTimer <= 4f)
                {
                    Rotate();
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void Shoot()
    {
        timer += Time.deltaTime;

        if (timer > 0.1f)
        {
            GameObject projectileClone = Instantiate(projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation);
            soundController.PlayGunSound(2);
            // Get the forward direction of the turret
            Vector2 turretForward = turret.transform.up;

            // Set the direction of the projectile based on the turret's forward direction
            projectileClone.GetComponent<ProjectileController>().SetDirection(turretForward);

            // Set other projectile properties
            projectileClone.GetComponent<Transform>().position = new Vector3(projectileClone.GetComponent<Transform>().position.x, projectileClone.GetComponent<Transform>().position.y, 10f);
            projectileClone.GetComponent<ProjectileController>().setProjectileHealth(100);
            projectileClone.GetComponent<ProjectileController>().setProjectileSpeed(10f);
            projectileClone.GetComponent<ProjectileController>().setProjectileDamage(100f);

            timer = 0;
        }
    }


    private void Rotate()
    {
        turret.transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);
        Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isShooting = true;
        }
    }
}
