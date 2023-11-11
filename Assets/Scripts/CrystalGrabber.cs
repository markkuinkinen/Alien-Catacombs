using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGrabber : MonoBehaviour
{
    public CircleCollider2D playerMagnet;
    public GameObject spriteObject;
    public CircleCollider2D circleCollider;
    public GameObject playerMagnetIcon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void grabAllCrystals()
    {
        playerMagnet.enabled = true;

        var timer = 0f;
        timer += Time.deltaTime;

        var originalMagnetSize = playerMagnet.radius;
        playerMagnet.radius = 150f;
        spriteObject.SetActive(false);
        circleCollider.enabled = false;

        if (timer > 10f)
        {
            playerMagnet.radius = originalMagnetSize;
            playerMagnet.enabled = false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            grabAllCrystals();
        }
    }
}
