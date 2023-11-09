using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PerkController : MonoBehaviour
{
    PlayerController playerController;
    GunController gunController;
    GameController gameController;
    SpawnController spawnController;
    UIController uiController;
    public Transform parentTransform;
    // 3 locations
    public Transform[] perkLocations;

    // 7 perks
    public GameObject[] perks;
    public CircleCollider2D magnetPerkCollider;

    
    //public List<GameObject> perksList = new List<GameObject>();
    public int[] PerkIndex = { 0, 1, 2, 3, 4, 5 };

    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        spawnController = FindObjectOfType<SpawnController>();
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UIController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void displayPerks()
    {
        List<int> availableIndices = new List<int>(PerkIndex); // Use PerkIndex array to store available indices

        int tempIndex = availableIndices.Count;

        int firstPerkIndex = Random.Range(0, tempIndex);
        int firstPerk = availableIndices[firstPerkIndex];
        perks[firstPerk].SetActive(true);
        perks[firstPerk].GetComponent<Transform>().position = perkLocations[0].position;
        availableIndices.RemoveAt(firstPerkIndex);
        tempIndex--;

        int secondPerkIndex = Random.Range(0, tempIndex);
        int secondPerk = availableIndices[secondPerkIndex];
        perks[secondPerk].SetActive(true);
        perks[secondPerk].GetComponent<Transform>().position = perkLocations[1].position;
        availableIndices.RemoveAt(secondPerkIndex);
        tempIndex--;

        int thirdPerkIndex = Random.Range(0, tempIndex);
        int thirdPerk = availableIndices[thirdPerkIndex];
        perks[thirdPerk].SetActive(true);
        perks[thirdPerk].GetComponent<Transform>().position = perkLocations[2].position;

    }

    public void SetPerksInactive()
    {
        for (int i = 0; i < perks.Length; i++)
        {
            perks[i].SetActive(false);
        }
        spawnController.restartSpawning();
    }

    public void AddMovespeed()
    {
        Debug.Log("movespeed added");
        uiController.isPaused = false;
        gameController.perkMenu.SetActive(false);
        gameController.movespeedPerkMultiplier += 0.05f;
        playerController.dodgeDuration -= 0.01f;
        SetPerksInactive();
        gameController.perkContinue();
    }

    public void AddHealth()
    {
        Debug.Log("health added");
        uiController.isPaused = false;
        SetPerksInactive();
        gameController.maxPlayerHealth += 10f;
        gameController.RestoreHealth();
        gameController.perkContinue();
    }

    public void AddROF()
    {
        Debug.Log("rof added");
        uiController.isPaused = false;
        SetPerksInactive();
        gunController.rateOfFireMultiplier += 0.1f;
        gameController.perkContinue();

    }

    public void addDamage()
    {
        Debug.Log("damage added");
        uiController.isPaused = false;
        SetPerksInactive();
        gunController.damageMultiplier += 0.1f;
        gameController.perkContinue();

    }

    public void addMagnetDistance()
    {
        Debug.Log("magnet distance added");
        uiController.isPaused = false;
        SetPerksInactive();
        magnetPerkCollider.radius += 1f;
        gameController.perkContinue();

    }

    public void addCrystalAmount()
    {
        Debug.Log("crystal amount added");
        uiController.isPaused = false;
        gameController.crystalPerkMultiplier += 0.1f;
        SetPerksInactive();
        gameController.perkContinue();

    }

    public void addEXPAmount()
    {
        Debug.Log("exp amount added");
        uiController.isPaused = false;
        SetPerksInactive();
        gameController.perkContinue();

    }
}
