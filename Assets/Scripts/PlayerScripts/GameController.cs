using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    UpgradeController upgradeController;
    UIController UIController;
    PlayerController player;
    [SerializeField]
    private int score;
    
    public int maxPlayerHealth = 100; // + upgrade controller
    public int currentHp;

    public int playerLevel = 0;
    [SerializeField]
    private float pointsToLevel = 5000;
    [SerializeField]
    private float currentExp = 0;
    public float totalExp;
    public Slider expSlider;
    public int currencyAmount;

    public bool inPlayScene;

    //upgrades
    static float damageMultiplier = 1f;

    void Start()
    {
        upgradeController = FindObjectOfType<UpgradeController>();
        UIController = FindObjectOfType<UIController>();
        player = FindObjectOfType<PlayerController>();
        currentHp = maxPlayerHealth;
    }

    public void addDamageMultiplier()
    {
        damageMultiplier += 1f;
    }

    public float returnDamageMultiplier()
    {
        return damageMultiplier;
    }

    void trackExp()
    {
        expSlider.maxValue = pointsToLevel;
        expSlider.value = currentExp;
    }

    public void addCurrency(int amount)
    {
        currencyAmount += amount;
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
        currentExp += expAmount;
        totalExp += expAmount;
    }

    /*public int returnLevel()
    {
        return playerLevel;
    }*/
    
    void Update()
    {
        if (inPlayScene)
        {
            trackExp();
            LevelUp();
            TrackDeath();
        }
    }
}
