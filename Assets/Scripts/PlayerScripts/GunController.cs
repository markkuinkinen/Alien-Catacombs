using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    // UI
    public GameObject standardGunPicture;
    public GameObject laserGunPicture;
    public GameObject rocketGunPicture;

    // Gun that the player is holding
    public Text ammoAmountText;
    public GameObject heldStandard;
    public GameObject heldRocket;
    public GameObject heldLaser;

    public GameObject projectileSpawnLocation;
    //prefabs for ammo types
    public GameObject normalAmmo;       // 0
    public GameObject rocketAmmo;       // 1 
    public GameObject laserAmmo;        // 2
    private GameObject[] ammoType;

    PlayerController player;

    [SerializeField]
    private int ammoAmount;

    private int currentGun;
    
    private int ammoDamage = 10;
    public int ammoHealth = 10;
    public float ammoSpeed = 5;   // 5 is base(standard)

    public float rateOfFire = 10f;
    public bool isShooting;
    private bool isShootingCoroutineRunning = false;
    
    void Start()
    {
        ammoType = new GameObject[] { normalAmmo, rocketAmmo, laserAmmo};
        currentGun = 0;
        player = FindObjectOfType<PlayerController>();
    }

    public int getAmmoDamage()
    {
        return ammoDamage;
    }

    private void revertGun()
    {
        if (currentGun != 0)
        {
            if (ammoAmount < 1)
            {
                currentGun = 0;
                ammoDamage = 10;
                ammoHealth = 10;
                ammoSpeed = 5;
            }
        }
    }

    public IEnumerator ShootingProjectiles()
    {
        // Check if the coroutine is already running
        if (isShootingCoroutineRunning)
        {
            yield break; // Exit the coroutine if it's already running
        }

        isShootingCoroutineRunning = true; // Set the flag to indicate the coroutine is running

        while (isShooting)
        {
            Shoot(player.projectileDirection);

            yield return new WaitForSeconds(1f / rateOfFire); // Calculate delay based on rateOfFire
        }

        isShootingCoroutineRunning = false; // Reset the flag when the coroutine finishes
    }

    public void Shoot(Vector2 direction)
    {
        GameObject projectileClone = Instantiate(ammoType[currentGun], projectileSpawnLocation.GetComponent<Transform>().position, projectileSpawnLocation.GetComponent<Transform>().rotation);
        projectileClone.GetComponent<ProjectileController>().SetDirection(direction);
        projectileClone.GetComponent<ProjectileController>().setProjectileHealth(ammoHealth);
        projectileClone.GetComponent<ProjectileController>().setProjectileSpeed(ammoSpeed);
        projectileClone.GetComponent<ProjectileController>().setProjectileDamage(ammoDamage);
        ammoAmount -= 1;
        //set health and projectile speed
    }

    public void SetGun(int gun)
    {
        currentGun = gun;
    }

    public void equipRocket()
    {
        Debug.Log("rocket gun grabbed");
        currentGun = 1;
        ammoAmount = 50;
        ammoDamage = 20;
        ammoSpeed = 1;
        ammoHealth = 10;
    }
    public void equipLaser()
    {
        Debug.Log("laser gun grabbed");
        currentGun = 2;
        ammoAmount = 10;
        ammoDamage = 50;
        ammoSpeed = 10;
        ammoHealth = 2000;
        //
    }

    private void OnTriggerEnter2D(Collider2D ammoType)
    {
        if (ammoType.tag == "RocketAmmo")
        {
            equipRocket();
            Destroy(ammoType.gameObject);
        }
        if (ammoType.tag == "LaserAmmo")
        {
            equipLaser();
            Destroy(ammoType.gameObject);
        }

    }

    void GunUISwapper()
    {
        if (currentGun == 0)    // Standard
        {
            standardGunPicture.SetActive(true);
            heldStandard.SetActive(true);

            laserGunPicture.SetActive(false);
            rocketGunPicture.SetActive(false);            
            heldLaser.SetActive(false);
            heldRocket.SetActive(false);

            ammoAmountText.text = "\u221E";
        }
        else if (currentGun == 1)   // Rocket
        {
            rocketGunPicture.SetActive(true);
            heldRocket.SetActive(true);
            
            standardGunPicture.SetActive(false);
            heldStandard.SetActive(false);
            laserGunPicture.SetActive(false);
            heldLaser.SetActive(false);

            ammoAmountText.text = ammoAmount.ToString();
        }
        else if (currentGun == 2)   //Laser
        {
            heldLaser.SetActive(true);
            laserGunPicture.SetActive(true);

            standardGunPicture.SetActive(false);
            rocketGunPicture.SetActive(false);
            heldStandard.SetActive(false);
            heldRocket.SetActive(false);

            ammoAmountText.text = ammoAmount.ToString();
        }
    }

    

    void Update()
    {
        revertGun();
        GunUISwapper();

        if (isShooting)
        {
            // Start the coroutine only if it's not already running
            if (!isShootingCoroutineRunning)
            {
                StartCoroutine(ShootingProjectiles());
            }
        }
        else
        {
            // Stop the coroutine only if it's running
            if (isShootingCoroutineRunning)
            {
                StopCoroutine(ShootingProjectiles());
                isShootingCoroutineRunning = false; // Reset the flag when stopping the coroutine
            }
        }
    }
}
