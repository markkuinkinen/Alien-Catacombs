using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroppedObject : MonoBehaviour
{
    private float timer;
    private Rigidbody2D rb;

    private float moveSpeed = 0.2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //To assure that the object can be seen when playing
        Vector3 startingPosition = transform.position;
        startingPosition.z = 1f;
        transform.position = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        moveUpDown();
    }

    void moveUpDown()
    {

        timer += Time.deltaTime;

        if (timer < 1f)
        {
            rb.velocity = new Vector3(0f, moveSpeed, 1f);// * Time.deltaTime);
        }
        else if (timer > 1f && timer < 2f)
        {
            rb.velocity = new Vector3(0f, -moveSpeed, 1f);
        }
        else
        {
            timer = 0f;
        }
    }
}
