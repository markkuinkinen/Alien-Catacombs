using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    UIController uiController;
    PlayerController player;

    public Sprite customCursorSprite; 
    public string sortingLayerName = "Default"; 
    public int orderInLayer = 10;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        uiController = FindObjectOfType<UIController>();
        Cursor.visible = false;
    }

    void Update()
    {
        if ((!uiController.isPaused || !uiController.playerIsPaused) && player.isAlive)
        {
            Cursor.visible = false;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (customCursorSprite != null)
            {
                GameObject cursorObject = GameObject.Find("CustomCursor");

                if (cursorObject == null)
                {
                    cursorObject = new GameObject("CustomCursor");
                    cursorObject.AddComponent<SpriteRenderer>().sprite = customCursorSprite;
                }

                SpriteRenderer spriteRenderer = cursorObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = customCursorSprite;
                spriteRenderer.sortingLayerName = sortingLayerName;
                spriteRenderer.sortingOrder = orderInLayer;
                spriteRenderer.color = Color.magenta;

                cursorObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            }
        }
        else
        {
            Cursor.visible = true;
        }
        
    }
}
