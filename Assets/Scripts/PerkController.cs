using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkController : MonoBehaviour
{
    PlayerController playerController;
    UIController uiController;
    // 3 locations
    public Transform[] perkLocations;

    // 7 perks
    public GameObject[] perks;

    // 
    public List<GameObject> perksList = new List<GameObject>();

    void Start()
    {
        UIController uiController = FindObjectOfType<UIController>();
        playerController = FindObjectOfType<PlayerController>();
        //List<GameObject> perksList = new List<GameObject> { perks[0], perks[1], perks[2], perks[3], perks[4], perks[5] };
        perksList.AddRange(perks);
        displayPerks();
    }

    // Update is called once per frame
    void Update()
    {
        //displayPerks();
    }

    void displayPerks()
    {
        int index = 5;

        int firstPerk = 0;// Random.Range(0, index);
        Instantiate(perksList[1], perkLocations[1]);
        perksList.Remove(perksList[firstPerk]);
        index -= 1;

        //int secondPerk = 1;// Random.Range(0, index);
        //Instantiate(perksList[secondPerk], perkLocations[1]);
        //perksList.Remove(perksList[secondPerk]);
        //index -= 1;

        //int thirdPerk = 2;//Random.Range(0, index);
        //Instantiate(perksList[thirdPerk], perkLocations[2]);
        //perksList.Remove(perksList[thirdPerk]);

        //perksList.RemoveAll(perksList.Contains);
        //resetPerks();
    }

    void resetPerks()
    {
        List<GameObject> perksList = new List<GameObject> { perks[0], perks[1], perks[2], perks[3], perks[4], perks[5] };
    }

    public void AddMovespeed()
    {
        Debug.Log("movespeed added");
        uiController.isPaused = false;
        uiController.LevelMenu.SetActive(false);
    }

    public void AddHealth()
    {
        Debug.Log("health added");
        uiController.isPaused = false;
        uiController.LevelMenu.SetActive(false);
    }

    public void AddROF()
    {
        Debug.Log("rof added");
        uiController.isPaused = false;
        uiController.LevelMenu.SetActive(false);
    }

    public void addDamage()
    {
        Debug.Log("damage added");
        uiController.isPaused = false;
        uiController.LevelMenu.SetActive(false);
    }

    public void addMagnetDistance()
    {
        Debug.Log("magnet distance added");
        uiController.isPaused = false;
        uiController.LevelMenu.SetActive(false);
    }

    public void addCrystalAmount()
    {
        Debug.Log("crystal amount added");
        uiController.isPaused = false;
        uiController.LevelMenu.SetActive(false);
    }

    public void addEXPAmount()
    {
        Debug.Log("exp amount added");
        uiController.isPaused = false;
        uiController.LevelMenu.SetActive(false);
    }
}
