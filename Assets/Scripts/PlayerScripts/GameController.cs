using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    UpgradeController upgradeController;
    GunController gunController;
    PerkController perkController;
    UIController UIController;
    PlayerController player;
    [SerializeField]
    private int score;
    
    public float maxPlayerHealth; // + upgrade controller
    public float currentHp;

    public int playerLevel = 1;
    [SerializeField]
    private float pointsToLevel = 100f;
    [SerializeField]
    private float currentExp = 0;
    public float totalExp;
    public Slider expSlider;
    public float currencyAmount;

    public bool inPlayScene;

    // Store upgrade multipliers
    static float damageMultiplier = 1f;
    static float expMultiplier = 1f;
    static float movespeedMultiplier = 1f;
    static float healthMultiplier = 1f;
    static float crystalMultiplier = 1f;
    static bool hasDash = false;

    // Perk upgrade multipliers
    public float damagePerkMultiplier = 0f;
    public float expPerkMultiplier = 0f;
    public float movespeedPerkMultiplier = 0f;
    public float healthPerkMultiplier = 0f;
    public float crystalPerkMultiplier = 0f;

    static float totalPlayerCrystals;

    public GameObject perkMenu;

    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        perkController = FindObjectOfType<PerkController>();
        upgradeController = FindObjectOfType<UpgradeController>();
        UIController = FindObjectOfType<UIController>();
        player = FindObjectOfType<PlayerController>();
        maxPlayerHealth = 100 * healthMultiplier;
        currentHp = maxPlayerHealth;
    }

    void Update()
    {
        if (inPlayScene)
        {
            trackExp();
            LevelUp();
            TrackDeath();
        }
    }

    public void RestoreHealth()
    {
        currentHp = maxPlayerHealth;
    }

    void trackExp()
    {
        expSlider.maxValue = pointsToLevel;
        expSlider.value = currentExp;
    }

    public void addCurrency(float amount)
    {
        currencyAmount += amount * (crystalMultiplier + crystalPerkMultiplier);
        totalPlayerCrystals += amount * (crystalMultiplier + crystalPerkMultiplier);
    }

    void LevelUp()
    {
        if (expSlider.value >= expSlider.maxValue)
        {
            UIController.playerIsPaused = true;
            UIController.enemyIsPaused = false;
            UIController.isPaused = true;
            gunController.isShooting = false;
            gunController.canShoot = false;

            perkMenu.SetActive(true);
            perkController.displayPerks();
            score += (int)currentExp;
            currentExp = 0;
            playerLevel += 1;
            pointsToLevel += (pointsToLevel * 0.15f);
        }
    }

    void TrackDeath()
    {
        if (currentHp <= 0)
        {
            player.isAlive = false;
            UIController.ActivateDeathMenu();
            gunController.isShooting = false;
            Debug.Log("youre dead");   
        }
    }

    public void perkContinue()
    {
        pointsToLevel += (pointsToLevel * 0.3f);
        UIController.isPaused = false;
        UIController.playerIsPaused = false;
        gunController.isShooting = false;
        gunController.canShoot = true;
        perkMenu.SetActive(false);
    }


    public void giveExp(float expAmount)
    {
        currentExp += expAmount * expMultiplier;
        totalExp += expAmount * expMultiplier;
    }

    public void removeCrystalAmount(int amount)
    {
        totalPlayerCrystals -= amount;
    }

    public float getTotalCrystals()
    {
        return totalPlayerCrystals;
    }


    #region MultiplierGetters&Setters

    public void addDamageMultiplier()
    {
        damageMultiplier += 0.1f;
    }

    public float returnDamageMultiplier()
    {
        return damageMultiplier;
    }

    public void addExpMultiplier()
    {
        expMultiplier += 0.1f;
    }

    public float returnExpMultiplier()
    {
        return expMultiplier;
    }

    public void addMovespeedMultiplier()
    {
        movespeedMultiplier += 0.05f;
    }

    public float returnMovespeedMultiplier()
    {
        return movespeedMultiplier + movespeedPerkMultiplier;
    }

    public void addHealthMultiplier()
    {
        healthMultiplier += 0.1f;
    }

    public float returnHealthMultiplier()
    {
        return healthMultiplier;
    }

    public void addCrystalMultiplier()
    {
        crystalMultiplier += 0.2f;
    }

    public float returnCrystalMultiplier()
    {
        return crystalMultiplier;
    }

    public void addDash()
    {
        hasDash = true;
    }

    public bool returnHasDash()
    {
        return hasDash;
    }

    #endregion

}
