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
    bool dashBought;

    //Text of price
    public Text damageCostText;
    public Text expCostText;
    public Text speedCostText;
    public Text healthCostText;
    public Text crystalCostText;
    public Text dashCostText;

    //Upgrade amounts
    public Text damageAmountText;
    public Text expAmountText;
    public Text speedAmountText;
    public Text healthAmountText;
    public Text crystalAmountText;
    public Text dashAmountText;

    public Text playerCrystalAmount;

    void Start()
    {
        //gameController = FindObjectOfType<GameController>();
        gunController = FindObjectOfType<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Store buttons for buying
    public void BuyDamage()
    {
        gameController.addDamageMultiplier();
    }

    public void BuyEXP()
    {

    }

    public void BuyMS()
    {

    }

    public void BuyHealth()
    {

    }

    public void BuyDropRate()
    {

    }

    public void BuyDash()
    {

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

}
