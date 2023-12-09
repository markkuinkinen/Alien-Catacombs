using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [SerializeField]
    private float enemySpawnTimer;
    public float spawnInterval = 2.5f;
    public int spawnCount = 3;
    public int spawnRange = 0;
    [SerializeField]
    private float crateTimer = 0f;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<PlayerController>();
        UIController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        if (!UIController.isPaused && player.isAlive)
        {
            crateTimer += Time.deltaTime;
            enemySpawnTimer += Time.deltaTime;
            SpawnCrates();
            spawnEnemiesOnTimer(enemySpawnTimer);
        }
    }

    void SpawnCrates()
    {
        if (crateTimer >= 13f)
        {
            var sideToSpawnOn = Random.Range(0, 2); //add up/down
            print("spawned crate");

            //Left side crate
            if (sideToSpawnOn == 0)
            {
                GameObject spawnedCrateLeft = Instantiate(cratePrefab, new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

            }
            else // Right side crate
            {
                GameObject spawnedCrateRight = Instantiate(cratePrefab, new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
            }
            crateTimer = 0f;
        }
    }

    public void restartSpawning()
    {
        Debug.Log("Starting spawn coroutine: line 82");
    }

    public void spawnEnemiesOnTimer(float interval)
    {
        spawnableEnemies = new GameObject[] { wormEnemy, eyeballEnemy, alienSoldierEnemy, eggBugEnemy };

        Vector3 leftSpawn = new Vector3(player.transform.position.x - 23f, Random.Range(player.transform.position.y - 13f, player.transform.position.y + 13f));
        Vector3 rightSpawn = new Vector3(player.transform.position.x + 23f, Random.Range(player.transform.position.y - 13f, player.transform.position.y + 13f));
        Vector3 upSpawn = new Vector3(Random.Range(player.transform.position.x - 21, player.transform.position.x + 21), player.transform.position.y + 15);
        Vector3 downSpawn = new Vector3(Random.Range(player.transform.position.x - 21, player.transform.position.x + 21), player.transform.position.y - 15);

        Vector3[] spawnPositions = new Vector3[] { leftSpawn, rightSpawn, upSpawn, downSpawn };

        if (interval > spawnInterval)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                var randomPosition = Random.Range(0, 4);
                var spawnNumber = Random.Range(0, spawnRange);
                GameObject spawnedEnemy = Instantiate(spawnableEnemies[spawnNumber], spawnPositions[randomPosition], Quaternion.identity);
            }
            enemySpawnTimer = 0;
        }
    }

        //if (interval > spawnInterval)
        //{
        //    if (gameController.playerLevel <= 3)    // spawn 2 every 2.5s
        //    {
        //        for (int i = 0; i < 2; i++)
        //        {
        //            var randomPosition = Random.Range(0, 4);
        //            if (randomPosition == 0)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], leftSpawn, Quaternion.identity);
        //            }
        //            else if (randomPosition == 1)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], rightSpawn, Quaternion.identity);
        //            }
        //            else if (randomPosition == 2)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], upSpawn, Quaternion.identity);
        //            }
        //            else
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], downSpawn, Quaternion.identity);
        //            }
        //        }
        //        enemySpawnTimer = 0f;
        //    }
        //    else if (gameController.playerLevel > 3 && gameController.playerLevel <= 7)
        //    {
        //        for (int i = 0; i < 3; i++) // spawns 3 enemies now, added range
        //        {
        //            var randomNumber = Random.Range(0, 4);
        //            var randomSpawn = Random.Range(0, 11);
        //            if (randomNumber == 0)
        //            {
        //                if (randomSpawn < 4)    // low chance of spawning level 1
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], leftSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], leftSpawn, Quaternion.identity);
        //                }
        //            }
        //            else if (randomNumber == 1)
        //            {
        //                if (randomSpawn < 4)    // low chance of spawning level 1
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], rightSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], rightSpawn, Quaternion.identity);
        //                }
        //            }
        //            else if (randomNumber == 2)
        //            {
        //                if (randomSpawn < 4)    // low chance of spawning level 1
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], upSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], upSpawn, Quaternion.identity);
        //                }
        //            }
        //            else
        //            {
        //                if (randomSpawn < 4)    // low chance of spawning level 1
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[0], downSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], downSpawn, Quaternion.identity);
        //                }
        //            }
        //        }
        //        enemySpawnTimer = 0f;
        //    }
        //    else if (gameController.playerLevel > 7 && gameController.playerLevel <= 10)
        //    {
        //        for (int i = 0; i < 4; i++) // spawns 3 enemies now, added range
        //        {
        //            var randomNumber = Random.Range(0, 4);
        //            var randomSpawn = Random.Range(0, 11);
        //            if (randomNumber == 0)
        //            {
        //                if (randomSpawn < 4)
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], leftSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(1, 3)], leftSpawn, Quaternion.identity);
        //                }
        //            }
        //            else if (randomNumber == 1)
        //            {
        //                if (randomSpawn < 4)
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], rightSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(1, 3)], rightSpawn, Quaternion.identity);
        //                }
        //            }
        //            else if (randomNumber == 2)
        //            {
        //                if (randomSpawn < 4)
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], upSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(1, 3)], upSpawn, Quaternion.identity);
        //                }
        //            }
        //            else
        //            {
        //                if (randomSpawn < 4)
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], downSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(1, 3)], downSpawn, Quaternion.identity);
        //                }
        //            }
        //        }
        //        enemySpawnTimer = 0f;
        //    }
        //    else if (gameController.playerLevel > 10 && gameController.playerLevel <= 15)
        //    {
        //        for (int i = 0; i < 5; i++) // spawns 3 enemies now, added range
        //        {
        //            var randomNumber = Random.Range(0, 4);
        //            var randomSpawn = Random.Range(0, 11);
        //            if (randomNumber == 0)
        //            {
        //                if (randomSpawn < 4)
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, 2)], downSpawn, Quaternion.identity);
        //                }
        //                else
        //                {
        //                    GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], downSpawn, Quaternion.identity);
        //                }
        //            }
        //            else if (randomNumber == 1)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], rightSpawn, Quaternion.identity);
        //            }
        //            else if (randomNumber == 2)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], upSpawn, Quaternion.identity);
        //            }
        //            else
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], downSpawn, Quaternion.identity);
        //            }
        //        }
        //        enemySpawnTimer = 0f;
        //    }
        //    else if (gameController.playerLevel > 15 && gameController.playerLevel <= 20)
        //    {
        //        for (int i = 0; i < 5; i++) // spawns 3 enemies now, added range
        //        {
        //            var randomNumber = Random.Range(0, 4);
        //            if (randomNumber == 0)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], leftSpawn, Quaternion.identity);
        //            }
        //            else if (randomNumber == 1)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], rightSpawn, Quaternion.identity);
        //            }
        //            else if (randomNumber == 2)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], upSpawn, Quaternion.identity);
        //            }
        //            else
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], downSpawn, Quaternion.identity);
        //            }
        //        }
        //        enemySpawnTimer = 0f;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 6; i++) // spawns 3 enemies now, added range
        //        {
        //            var randomNumber = Random.Range(0, 4);
        //            if (randomNumber == 0)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], leftSpawn, Quaternion.identity);
        //            }
        //            else if (randomNumber == 1)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], rightSpawn, Quaternion.identity);
        //            }
        //            else if (randomNumber == 2)
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], upSpawn, Quaternion.identity);
        //            }
        //            else
        //            {
        //                GameObject spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(2, 4)], downSpawn, Quaternion.identity);
        //            }
        //        }
        //        enemySpawnTimer = 0f;
        //    }
        //    enemySpawnTimer = 0f;
        //}

    //}

    //this works for left and right side
    //public void spawnEnemiesOnTimer(float interval)
    //{
    //    spawnableEnemies = new GameObject[] { wormEnemy, eyeballEnemy, alienSoldierEnemy, eggBugEnemy }; //bossEnemy };

    //    if (interval > spawnInterval)
    //    {
    //        if (gameController.playerLevel <= 3)    // spawn 2 every 2.5s
    //        {
    //            Debug.Log("first level spawned <=3 " + gameController.playerLevel);
    //            // Left side spawner
    //            GameObject spawnedEnemy1 = Instantiate(spawnableEnemies[0], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //            //Right side spawner
    //            GameObject secondSpawnedEnemy2 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //        }
    //        else if (gameController.playerLevel > 3 && gameController.playerLevel <= 7)
    //        {
    //            Debug.Log("second level spawned 4-7" + gameController.playerLevel);
    //            GameObject spawnedEnemy3 = Instantiate(spawnableEnemies[Random.Range(0, 2)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject secondSpawnedEnemy4 = Instantiate(spawnableEnemies[Random.Range(0, 2)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //            GameObject previousSpawnedEnemy = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //        }
    //        else if (gameController.playerLevel > 7 && gameController.playerLevel <= 10)
    //        {
    //            Debug.Log("third level spawned " + gameController.playerLevel);
    //            GameObject spawnedEnemy5 = Instantiate(spawnableEnemies[Random.Range(1, 3)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject secondSpawnedEnemy6 = Instantiate(spawnableEnemies[Random.Range(1, 3)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy1 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy2 = Instantiate(spawnableEnemies[1], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //        }
    //        else if (gameController.playerLevel > 10 && gameController.playerLevel <= 15)
    //        {
    //            Debug.Log("fourth level spawned" + gameController.playerLevel);
    //            GameObject spawnedEnemy7 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //            GameObject secondSpawnedEnemy8 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy3 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy4 = Instantiate(spawnableEnemies[1], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy5 = Instantiate(spawnableEnemies[2], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //        }
    //        else if (gameController.playerLevel > 15 && gameController.playerLevel <= 20)
    //        {
    //            Debug.Log("fifth level spawned" + gameController.playerLevel);
    //            GameObject spawnedEnemy9 = Instantiate(spawnableEnemies[Random.Range(3, 5)], new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //            GameObject secondSpawnedEnemy10 = Instantiate(spawnableEnemies[Random.Range(3, 5)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy6 = Instantiate(spawnableEnemies[0], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy7 = Instantiate(spawnableEnemies[1], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy8 = Instantiate(spawnableEnemies[3], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy9 = Instantiate(spawnableEnemies[3], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //        }
    //        else
    //        {
    //            Debug.Log("final level spawned >20" + gameController.playerLevel);
    //            GameObject previousSpawnedEnemy10 = Instantiate(spawnableEnemies[Random.Range(1, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy11 = Instantiate(spawnableEnemies[Random.Range(1, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy12 = Instantiate(spawnableEnemies[Random.Range(1, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy13 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy14 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
    //            GameObject previousSpawnedEnemy15 = Instantiate(spawnableEnemies[Random.Range(2, 4)], new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

    //        }
    //        enemySpawnTimer = 0f;
    //    }

    //}

}

