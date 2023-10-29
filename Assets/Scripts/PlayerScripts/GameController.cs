using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    UIController UIController;
    PlayerController player;
    [SerializeField]
    private int score;
    private int playerHealth = 3;
    private int currentHp = 0;

    public int playerLevel = 0;
    [SerializeField]
    private float pointsToLevel = 5000;
    [SerializeField]
    private float currentExp = 0;
    public float totalExp;
    public Slider expSlider;
    public int currencyAmount;

    void Start()
    {
        UIController = FindObjectOfType<UIController>();
        player = FindObjectOfType<PlayerController>();
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

    public void perkContinue()
    {
        playerLevel += 1;
        Debug.Log("leveled up and yeah");
        currentExp = 0;
        pointsToLevel += (pointsToLevel * 0.15f);
        UIController.LevelMenu.SetActive(false);
        UIController.isPaused = false;
    }

    void increaseMovespeed()
    {
        //PlayerController.increaseMoveSpeed();
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
        trackExp();
        LevelUp();
    }
}
