using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    UIController UIController;
    GameController gameController;
    PlayerController player;
    [SerializeField]
    private GameObject enemyPrefab;

    public GameObject wormEnemy;           //0
    public GameObject eyeballEnemy;        //1
    public GameObject alienSoldierEnemy;   //2
    public GameObject eggBugEnemy;         //3     TODO
    public GameObject bossEnemy;           //4     TODO
    private GameObject[] spawnableEnemies;

    private float spawnInterval = 2.5f;


    //ADD MORE ENEMIES WITH DIFFERENT HP/SPEED/LOOK


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<PlayerController>();
        UIController = FindObjectOfType<UIController>();
        StartCoroutine(spawnEnemies(spawnInterval, enemyPrefab));
        StartCoroutine(spawnEnemies(spawnInterval, enemyPrefab));

        spawnableEnemies = new GameObject[] {wormEnemy, eyeballEnemy, alienSoldierEnemy, eggBugEnemy, bossEnemy };
    }

    private void Update()
    {
        if (UIController.isPaused)
        {
            StopAllCoroutines();
        }
        else if (!UIController.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(spawnEnemies(spawnInterval, enemyPrefab));
                StartCoroutine(spawnEnemies(spawnInterval, enemyPrefab));
            }
        }

    }

    private IEnumerator spawnEnemies(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);

        //make array of positions and random range it
        //spawns enemies to the left side of the player camera
        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(player.GetComponent<Transform>().position.x - 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);

        //spawns enemies to the right side of the player camera 
        GameObject secondSpawnedEnemy = Instantiate(enemy, new Vector3(player.transform.position.x + 23f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
        StartCoroutine(spawnEnemies(interval, enemy));
    }

}

