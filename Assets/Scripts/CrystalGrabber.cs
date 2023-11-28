using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGrabber : MonoBehaviour
{
    public CircleCollider2D playerMagnet;
    public GameObject spriteObject;
    public CircleCollider2D circleCollider;
    public GameObject playerMagnetIcon;

    public float timer;
    [SerializeField]
    private bool magnetOn;

    void Start()
    {
    }


    void Update()
    {
        if (magnetOn)
        {
            timer += Time.deltaTime;
            grabAllCrystals();

        }
    }

    void grabAllCrystals()
    {
        var originalMagnetSize = playerMagnet.radius;
        playerMagnet.radius = 150f;
        spriteObject.SetActive(false);
        circleCollider.enabled = false;
        playerMagnetIcon.SetActive(true);

        if (timer > 3.5f)
        {
            playerMagnet.radius = originalMagnetSize;
            playerMagnetIcon.SetActive(false);
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
