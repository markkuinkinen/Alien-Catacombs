using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    UIController UIController;
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
    GameController gameController;

    [SerializeField]
    private int ammoAmount;

    private int currentGun;

    public float damageMultiplier = 1f;
    private float currentAmmoDamage;
    [SerializeField]
    private float baseAmmoDamage = 5f;
    public int ammoHealth = 10;
    public float ammoSpeed = 15;   // 5 is base(standard)

    [SerializeField]
    private float rateOfFire = 0.35f;
    public float rateOfFireMultiplier = 0f;
    public bool isShooting;
    public bool canShoot;
    public float lastShotTime;
    private bool isShootingCoroutineRunning = false;

    public bool timeStopped;
    private float timeStopMultiplier;

    void Start()
    {
        UIController = FindObjectOfType<UIController>();
        gameController = FindObjectOfType<GameController>();
        ammoType = new GameObject[] { normalAmmo, rocketAmmo, laserAmmo};
        currentGun = 0;
        player = FindObjectOfType<PlayerController>();
        canShoot = true;
    }

    

    public string getCurrentGun()
    {
        if (currentGun == 0)
        {
            return "Standard";
        }
        else if (currentGun == 1)
        {
            return "Rocket";
        }
        else if (currentGun == 2)
        {
            return "Laser";
        }
        return "no gun equipped somehow";
    }


    public float getCurrentAmmoDamage()
    {
        return currentAmmoDamage;
    }
    public float getBaseAmmoDamage()
    {
        return baseAmmoDamage;
    }

    private void revertGun()
    {
        if (currentGun != 0)
        {
            if (ammoAmount < 1)
            {
                currentGun = 0;
                ammoHealth = 10;
                ammoSpeed = 15;
                rateOfFire = 0.3f;
                baseAmmoDamage = 5;
            }
        }
    }

    public IEnumerator ShootingProjectiles()
    {
        if (isShootingCoroutineRunning)
        {
            yield break; 
        }

        while (isShooting)
        {
            float elapsedTime = Time.time - lastShotTime;

            if (elapsedTime >= RofAdjusted())
            {
                Shoot(player.projectileDirection);
                lastShotTime = Time.time;
            }
            yield return null;
        }
    }

    public float RofAdjusted()
    {
        return rateOfFire - (rateOfFire * rateOfFireMultiplier);
    }

    public IEnumerator ShootingCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(RofAdjusted());
        canShoot = true;
    }

    public void Shoot(Vector2 direction)
    {
        GameObject projectileClone = Instantiate(ammoType[currentGun], projectileSpawnLocation.GetComponent<Transform>().position, projectileSpawnLocation.GetComponent<Transform>().rotation);
        projectileClone.GetComponent<ProjectileController>().SetDirection(direction);
        projectileClone.GetComponent<ProjectileController>().setProjectileHealth(ammoHealth);
        projectileClone.GetComponent<ProjectileController>().setProjectileSpeed(ammoSpeed);
        projectileClone.GetComponent<ProjectileController>().setProjectileDamage((currentAmmoDamage * damageMultiplier) * timeStopMultiplier);
        projectileClone.GetComponent<ProjectileController>().SetAmmoType(getCurrentGun());
        ammoAmount -= 1;
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
        ammoSpeed = 1;
        ammoHealth = 1;
        rateOfFire = 0.9f;
        baseAmmoDamage = 20;
    }
    public void equipLaser()
    {
        Debug.Log("laser gun grabbed");
        currentGun = 2;
        ammoAmount = 50;
        ammoSpeed = 10;
        ammoHealth = 2000;
        rateOfFire = 0.7f;
        baseAmmoDamage = 30;
    }

    private void OnTriggerEnter2D(Collider2D ammoType)
    {
        if (ammoType.tag == "RocketAmmo")
        {
            if (currentGun == 0 || currentGun == 2)
            {
                equipRocket();
            }
            else
            {
                ammoAmount += 150;
            }
            Destroy(ammoType.gameObject);
        }
        if (ammoType.tag == "LaserAmmo")
        {
            if (currentGun == 0 || currentGun == 1)
            {
                equipLaser();
            }
            else
            {
                ammoAmount += 150;
            }
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

    void timeStopChecker()
    {


        if (timeStopped)
        {
            timeStopMultiplier = 5f;
        }
        else
        {
            timeStopMultiplier = 1f;
        }
    }

    void Update()
    {
        revertGun();
        GunUISwapper();
        timeStopChecker();

        // To apply the multiplier from the store 
        currentAmmoDamage = baseAmmoDamage * gameController.returnDamageMultiplier();


        if (isShooting && !UIController.playerIsPaused && !UIController.isPaused)
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
