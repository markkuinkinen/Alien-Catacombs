using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGrabber : MonoBehaviour
{
    public CircleCollider2D playerMagnet;
    public GameObject spriteObject;
    public CircleCollider2D circleCollider;
    public GameObject playerMagnetIcon;

    public float f;
    private bool magnetOn;

    void Start()
    {
    }


    void Update()
    {
        if (magnetOn)
        {
            var timer = 0f;
            timer += Time.deltaTime;

            if (timer <= 3f)
            {
                grabAllCrystals();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void grabAllCrystals()
    {
        var timer = 0f;
        timer += Time.deltaTime;
        f = timer;

        var originalMagnetSize = playerMagnet.radius;
        playerMagnet.radius = 150f;
        spriteObject.SetActive(false);
        circleCollider.enabled = false;
        playerMagnetIcon.SetActive(true);

        if (timer > 7.5f)
        {
            playerMagnet.radius = originalMagnetSize;
            playerMagnet.enabled = false;
            Destroy(this.gameObject);
            magnetOn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            magnetOn = true;
        }
    }
}
