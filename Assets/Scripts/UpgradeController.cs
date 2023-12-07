using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeController : MonoBehaviour
{
    public GameController gameController;
    GunController gunController;

    //Multiplier variables
    float damageMultiplier;
    float expMultiplier;
    float speedMultiplier;
    float healthMultiplier;
    float crystalMultiplier;
    

    //Text of price
    public Text damageCostText;
    public Text expCostText;
    public Text speedCostText;
    public Text healthCostText;
    public Text crystalCostText;
    public Text dashCostText;

    // Upgrade costs, changed after buying
    static int damageCost = 500;
    static int expCost = 500;
    static int speedCost = 500;
    static int healthCost = 500;
    static int crystalCost = 500;
    static int dashCost = 1000;

    //Upgrade amounts
    public Text damageAmountText;
    public Text expAmountText;
    public Text speedAmountText;
    public Text healthAmountText;
    public Text crystalAmountText;
    public Text dashAmountText;

    static int damageAmountBought = 0;      // x/3
    static int expAmountBought = 0;         // x/3
    static int speedAmountBought = 0;       // x/3
    static int healthAmountBought = 0;      // x/2
    static int crystalAmountBought = 0;     // x/2
    static int dashAmountBought = 0;

    public Text playerCrystalAmount;

    void Start()
    {
        //gameController = FindObjectOfType<GameController>();
        gunController = FindObjectOfType<GunController>();
    }

    void Update()
    {
        UpdateStoreText();
    }

    void UpdateStoreText()
    {
        // For the left side of the store -> X/X
        damageAmountText.text = damageAmountBought + "/3";
        expAmountText.text = expAmountBought + "/3";
        speedAmountText.text = speedAmountBought + "/3";
        healthAmountText.text = healthAmountBought + "/2";
        crystalAmountText.text = crystalAmountBought + "/2";
        dashAmountText.text = dashAmountBought + "/1";

        // To update the cost
        damageCostText.text = damageCost.ToString();
        expCostText.text = expCost.ToString();
        speedCostText.text = speedCost.ToString();
        healthCostText.text = healthCost.ToString();
        crystalCostText.text = crystalCost.ToString();
        dashCostText.text = dashCost.ToString();

        playerCrystalAmount.text = gameController.getTotalCrystals().ToString();
    }

    #region ButtonsForMenu

    // Store buttons for buying
    public void BuyDamage()
    {
        int maxAmount = 3;
        if (damageAmountBought < maxAmount && gameController.getTotalCrystals() >= damageCost)
        {
            gameController.addDamageMultiplier();
            gameController.removeCrystalAmount(damageCost);
            damageAmountBought++;
            Debug.Log("damagebought");
        }
        else
        {
            Debug.Log("not enough");
        }
    }

    public void BuyEXP()
    {
        int maxAmount = 3;
        if (expAmountBought < maxAmount && gameController.getTotalCrystals() >= damageCost)
        {
            gameController.removeCrystalAmount(damageCost);
            expAmountBought++;
            gameController.addExpMultiplier();
            Debug.Log("exp bought");
        }
        else
        {
            Debug.Log("not enough");
        }
    }

    public void BuyMS()
    {
        int maxAmount = 3;
        if (speedAmountBought < maxAmount && gameController.getTotalCrystals() >= speedCost)
        {
            gameController.removeCrystalAmount(speedCost);
            gameController.addMovespeedMultiplier();
            speedAmountBought++;
            Debug.Log("speed bought");
        }
        else
        {
            Debug.Log("not enough");
        }
    }

    public void BuyHealth()
    {
        int maxAmount = 2;
        if (healthAmountBought < maxAmount && gameController.getTotalCrystals() >= healthCost)
        {
            gameController.removeCrystalAmount(healthCost);
            gameController.addHealthMultiplier();
            healthAmountBought++;
            Debug.Log("health bought");
        }
        else
        {
            Debug.Log("not enough");
        }
    }

    public void BuyDropRate()
    {
        int maxAmount = 2;
        if (crystalAmountBought < maxAmount && gameController.getTotalCrystals() >= crystalCost)
        {
            gameController.removeCrystalAmount(crystalCost);
            gameController.addCrystalMultiplier();
            crystalAmountBought++;
            Debug.Log("crystal bought");
        }
        else
        {
            Debug.Log("not enough");
        }
    }

    public void BuyDash()
    {
        int maxAmount = 1;
        if (dashAmountBought < maxAmount && gameController.getTotalCrystals() >= dashCost)
        {
            gameController.removeCrystalAmount(dashCost);
            gameController.addDash();
            dashAmountBought++;
            Debug.Log("dash bought");
        }
        else
        {
            Debug.Log("not enough");
        }
    }

    // Store buttons for returning to menu and playing from store

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

}
