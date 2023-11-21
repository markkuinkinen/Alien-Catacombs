using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    UIController UIController;
    GameController gameController;
    PlayerController player;

    public GameObject wormEnemy;           //0
    public GameObject eyeballEnemy;        //1
    public GameObject alienSoldierEnemy;   //2
    public GameObject eggBugEnemy;         //3     TODO
    //public GameObject bossEnemy;           //4     TODO
    [SerializeField]
    private GameObject[] spawnableEnemies;

    public GameObject cratePrefab;

    private float spawnInterval = 2.5f;


    //ADD MORE ENEMIES WITH DIFFERENT HP/SPEED/LOOK


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<PlayerController>();
        UIController = FindObjectOfType<UIController>();
        StartCoroutine(spawnEnemies(spawnInterval));
        StartCoroutine(SpawnCrates());
    }

    private void Update()
    {
        if (UIController.isPaused || !player.isAlive)
        {
            StopAllCoroutines();
        }
        else if (!UIController.isPaused && player.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(spawnEnemies(spawnInterval));
            }
        }

        /*if (gameController.playerLevel > 4 && gameController.playerLevel % 5 == 0)
        {
            StartCoroutine(spawnEnemies(spawnInterval));
            Debug.Log("started extra coroutine");
            spawnInterval -= 0.2f;
        }*/
    }

    private IEnumerator SpawnCrates()
    {
        yield return new WaitForSeconds(10f);

        var sideToSpawnOn = Random.Range(0, 2); //add up/down

        //Left side crate
        if (sideToSpawnOn == 0)
        {
            GameObject spawnedCrateLeft = Instantiate(cratePrefab, new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

        }
        else // Right side crate
        {
            GameObject spawnedCrateRight = Instantiate(cratePrefab, new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
        }
    }

    public void restartSpawning()
    {
        StartCoroutine(spawnEnemies(spawnInterval));
        StartCoroutine(SpawnCrates());
    }

    //spawnableEnemies = new GameObject[] {wormEnemy, eyeballEnemy, alienSoldierEnemy, eggBugEnemy, bossEnemy };
    private IEnumerator spawnEnemies(float interval)
    {
        spawnableEnemies = new GameObject[] { wormEnemy, eyeballEnemy, alienSoldierEnemy, eggBugEnemy }; //bossEnemy };

        while (true)
        {
            if (gameController.playerLevel <= 3)
            {
                Debug.Log("first level spawned <=3 " + gameController.playerLevel);
                // Left side spawner
                GameObject spawnedEnemy1 = Instantiate(spawnableEnemies[0], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

                //Right side spawner
                GameObject secondSpawnedEnemy2 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
            }
            else if (gameController.playerLevel > 3 && gameController.playerLevel <= 7)
            {
                Debug.Log("second level spawned 4-7" + gameController.playerLevel);
                GameObject spawnedEnemy3 = Instantiate(spawnableEnemies[Random.Range(0, 2)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject secondSpawnedEnemy4 = Instantiate(spawnableEnemies[Random.Range(0, 2)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

                GameObject previousSpawnedEnemy = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

            }
            else if (gameController.playerLevel > 7 && gameController.playerLevel <= 10)
            {
                Debug.Log("third level spawned " + gameController.playerLevel);
                GameObject spawnedEnemy5 = Instantiate(spawnableEnemies[Random.Range(1, 3)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject secondSpawnedEnemy6 = Instantiate(spawnableEnemies[Random.Range(1, 3)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy1 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy2 = Instantiate(spawnableEnemies[1], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

            }
            else if (gameController.playerLevel > 10 && gameController.playerLevel <= 15)
            {
                Debug.Log("fourth level spawned" + gameController.playerLevel);
                GameObject spawnedEnemy7 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

                GameObject secondSpawnedEnemy8 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy3 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy4 = Instantiate(spawnableEnemies[1], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy5 = Instantiate(spawnableEnemies[2], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

            }
            else if (gameController.playerLevel > 15 && gameController.playerLevel <= 20)
            {
                Debug.Log("fifth level spawned" + gameController.playerLevel);
                GameObject spawnedEnemy9 = Instantiate(spawnableEnemies[Random.Range(3, 5)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

                GameObject secondSpawnedEnemy10 = Instantiate(spawnableEnemies[Random.Range(3, 5)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy6 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy7 = Instantiate(spawnableEnemies[1], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy8 = Instantiate(spawnableEnemies[3], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy9 = Instantiate(spawnableEnemies[3], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
            }
            else
            {
                Debug.Log("final level spawned >20" + gameController.playerLevel);
                GameObject previousSpawnedEnemy10 = Instantiate(spawnableEnemies[Random.Range(1, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy11 = Instantiate(spawnableEnemies[Random.Range(1, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy12 = Instantiate(spawnableEnemies[Random.Range(1, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy13 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy14 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
                GameObject previousSpawnedEnemy15 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

            }


            
            yield return new WaitForSeconds(interval);
        }

    }

}

