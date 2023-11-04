using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    UpgradeController upgradeController;
    UIController UIController;
    PlayerController player;
    [SerializeField]
    private int score;
    
    public float maxPlayerHealth; // + upgrade controller
    public float currentHp;

    public int playerLevel = 0;
    [SerializeField]
    private float pointsToLevel = 5000;
    [SerializeField]
    private float currentExp = 0;
    public float totalExp;
    public Slider expSlider;
    public float currencyAmount;

    public bool inPlayScene;

    //upgrades
    static float damageMultiplier = 1f;
    static float expMultiplier = 1f;
    static float movespeedMultiplier = 1f;
    static float healthMultiplier = 1f;
    static float crystalMultiplier = 1f;
    static bool hasDash = false;

    static float totalPlayerCrystals;

    void Start()
    {
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


    void trackExp()
    {
        expSlider.maxValue = pointsToLevel;
        expSlider.value = currentExp;
    }

    public void addCurrency(float amount)
    {
        currencyAmount += amount * crystalMultiplier;
        totalPlayerCrystals += amount * crystalMultiplier;
    }

    void LevelUp()
    {
        if (expSlider.value >= expSlider.maxValue)
        {
            UIController.isPaused = true;
            UIController.LevelMenu.SetActive(true);
            //playerLevel += 1;
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                playerLevel += 1;
                Debug.Log("leveled up and yeah");
                currentExp = 0;
                pointsToLevel += (pointsToLevel * 0.15f);
                UIController.isPaused = false;
            }*/
        }
    }

    void TrackDeath()
    {
        if (currentHp <= 0)
        {
            player.isAlive = false;
            UIController.ActivateDeathMenu();
            Debug.Log("youre dead");   
        }
    }

    public void perkContinue()
    {
        playerLevel += 1;
        Debug.Log("leveled up and yeah");
        currentExp = 0;
        pointsToLevel += (pointsToLevel * 0.15f);
        UIController.LevelMenu.SetActive(false);
        UIController.isPaused = false;
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
        return movespeedMultiplier;
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
