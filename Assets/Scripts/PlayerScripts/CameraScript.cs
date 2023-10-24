using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    PlayerController player;
    Transform trans;

    float xPos;
    float yPos;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        //followPlayer();
        MoveX();
    }


    void MoveX()    //tracks player up until the border 
    {

        if (player.transform.position.x > -52 && player.transform.position.x < 52)
        {
            xPos = player.transform.position.x;
        }

        if (player.transform.position.y > -35 && player.transform.position.y < 35)
        {
            yPos = player.transform.position.y;
        }

        trans.position = new Vector2(xPos, yPos);
    }
}
