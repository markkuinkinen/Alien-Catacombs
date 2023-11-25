using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject turret;
    public Transform projectileSpawnLocation;
    private bool isShooting;
    private float rotationSpeed = 160f;

    public float timer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            StartCoroutine(Rotating());
        }

    }

    void shooting()
    {
        timer += Time.deltaTime;

        if (timer > 0.1f)
        {
            GameObject projectileClone = Instantiate(projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation);

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


    IEnumerator Rotating()
    {
        turret.transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);
        shooting();
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isShooting = true;
        }
    }
}
