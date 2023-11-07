using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkController : MonoBehaviour
{
    PlayerController playerController;
    UIController uiController;
    public Transform parentTransform;
    // 3 locations
    public Transform[] perkLocations;

    // 7 perks
    public GameObject[] perks;

    // 
    public List<GameObject> perksList = new List<GameObject>();

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
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

        int firstPerk = 0;

        perksList[firstPerk].SetActive(true);
        perksList[firstPerk].GetComponent<Transform>().position = perkLocations[0].position;

        int secondPerk = 1;

        perksList[secondPerk].SetActive(true);
        perksList[secondPerk].GetComponent<Transform>().position = perkLocations[1].position;

        int thirdPerk = 2;

        perksList[thirdPerk].SetActive(true);
        perksList[thirdPerk].GetComponent<Transform>().position = perkLocations[2].position;

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
