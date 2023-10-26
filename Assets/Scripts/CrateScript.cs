using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public GameObject droppedLaser;
    public GameObject droppedRocket;

    private GameObject[] droppables;
    void Update()
    {
        
    }

    private void Start()
    {
        droppables = new GameObject[] { droppedLaser, droppedRocket };
    }

    void dropSomething()
    {
        GameObject droppedItem = Instantiate(droppables[Random.Range(0, 2)], this.transform.position, this.transform.rotation);
        droppedItem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            dropSomething();
            Destroy(collision.gameObject);
        }
    }
}
