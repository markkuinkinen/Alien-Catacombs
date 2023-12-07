using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public GameObject droppedLaser;
    public GameObject droppedRocket;
    public GameObject droppedBeer;
    public GameObject killObject;
    public GameObject timeStop;
    public GameObject MagnetDrop;

    private GameObject[] droppables;

    private bool hasDropped;
    void Update()
    {
        
    }

    private void Start()
    {

    }

    void dropSomething()
    {
        if (!hasDropped)
        {
            droppables = new GameObject[] { droppedLaser, droppedRocket, droppedBeer, killObject, timeStop, MagnetDrop };

            GameObject droppedItem = Instantiate(droppables[Random.Range(0, 6)], this.transform.position, this.transform.rotation);
            droppedItem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
            hasDropped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);

            dropSomething();
            Destroy(this.gameObject);

        }
    }
}
