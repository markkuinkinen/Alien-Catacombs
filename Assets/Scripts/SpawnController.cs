using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    UIController UIController;
    PlayerController player;
    [SerializeField]
    private GameObject enemyPrefab;

    private float spawnInterval = 1f;


    //ADD MORE ENEMIES WITH DIFFERENT HP/SPEED/LOOK


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        UIController = FindObjectOfType<UIController>();
        StartCoroutine(spawnEnemies(spawnInterval, enemyPrefab));
        StartCoroutine(spawnEnemies(spawnInterval, enemyPrefab));
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

        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(player.GetComponent<Transform>().position.x - 13f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
        GameObject secondSpawnedEnemy = Instantiate(enemy, new Vector3(player.transform.position.x + 13f, Random.Range(-13f, 13f), 0f), Quaternion.identity);
        StartCoroutine(spawnEnemies(interval, enemy));
    }

}

