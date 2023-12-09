using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopController : MonoBehaviour
{
    UIController uiController;
    GunController gunController;
    public GameObject clockSprite;
    CircleCollider2D HitCollider;
    public float timer;
    private bool timeStopped;
    void Start()
    {
        HitCollider = GetComponent<CircleCollider2D>();
        uiController = FindObjectOfType<UIController>();
        gunController = FindObjectOfType<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStopped)
        {
            timer += Time.deltaTime;
            StopTime();
        }
    }

    void StopTime()
    {
        uiController.enemyIsPaused = true;
        clockSprite.SetActive(false);
        HitCollider.enabled = false;
        gunController.timeStopped = true;
        if (timer >= 8f)
        {
            gunController.timeStopped = false;
            uiController.enemyIsPaused = false;
            timeStopped = false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timeStopped = true;
        }
    }

}
