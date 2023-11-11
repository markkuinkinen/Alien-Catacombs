using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopController : MonoBehaviour
{
    UIController uiController;
    public GameObject clockSprite;
    CircleCollider2D HitCollider;
    public float timer;
    private bool timeStopped;
    void Start()
    {
        HitCollider = GetComponent<CircleCollider2D>();
        uiController = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStopped)
        {
            StopTime();
        }
    }

    void StopTime()
    {
        timer += Time.deltaTime;

        uiController.isPaused = true;
        clockSprite.SetActive(false);
        HitCollider.enabled = false;
        if (timer >= 7.5f)
        {
            uiController.isPaused = false;
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
