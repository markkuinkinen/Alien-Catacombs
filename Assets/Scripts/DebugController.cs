using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{

    public Text damageText;
    public Text expText;
    public Text moveSpeedText;
    public Text healthText;
    public Text crystalText;
    
    GameController gameController;
    GunController gunController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gunController = FindObjectOfType<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Current gun, current damage, damage multiplier
        damageText.text = "Current gun damage: " + gunController.getCurrentAmmoDamage() + " || Damage multiplier: " + gameController.returnDamageMultiplier() + " || Base: " + gunController.getCurrentAmmoDamage();

        //// base exp, Exp multiplier
        //expText.text = "";

        //// Base movement speed, movement speed multiplier, current movement speed
        //moveSpeedText.text = "";

        //// base health, total health, current health
        //healthText.text = "";

        //// total crystals, current crystals, crystal multiplier
        //crystalText.text = "";

    }
}
