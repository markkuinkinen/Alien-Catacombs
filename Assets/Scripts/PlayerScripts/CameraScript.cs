using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        followPlayer();
    }

    void followPlayer()
    {
        this.GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}
