using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGrabber : MonoBehaviour
{

    public GameObject spriteObject;
    public CircleCollider2D circleCollider;

    public GameObject playerMagnetIcon;
    public CircleCollider2D playerMagnet;
    public PlayerController player;
    public float timer;
    public float playerMagnetRadius;
    
    [SerializeField]
    private bool magnetOn;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        playerMagnet = player.transform.Find("PlayerMagnet").GetComponent<CircleCollider2D>();
        Transform middleOfPlayer = player.transform.Find("MiddleOfPlayer");
        playerMagnetIcon = middleOfPlayer.Find("MagnetIcon").gameObject;
        //if (player != null)
        //{
        //    playerMagnet = player.transform.Find("PlayerMagnet").GetComponent<CircleCollider2D>();

        //    if (playerMagnet != null)
        //    {
        //        Transform middleOfPlayer = player.transform.Find("MiddleOfPlayer");

        //        if (middleOfPlayer != null)
        //        {
        //            playerMagnetIcon = middleOfPlayer.Find("MagnetIcon").gameObject;
        //        }
        //        else
        //        {
        //            Debug.LogError("MiddleOfPlayer not found!");
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("PlayerMagnet GameObject not found on the player!");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Player object not found!");
        //}
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
       // var originalMagnetSize = playerMagnet.radius;
        playerMagnet.radius = 150f;
        spriteObject.SetActive(false);
        circleCollider.enabled = false;
        playerMagnetIcon.SetActive(true);

        if (timer > 4f)
        {
            playerMagnet.radius = playerMagnetRadius;
            playerMagnetIcon.SetActive(false);
            magnetOn = false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            magnetOn = true;
            playerMagnetRadius = playerMagnet.radius;
        }
    }
}
