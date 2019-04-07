using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    private BoundsCheck _bndsCheck;

    void Start()
    { 
        _bndsCheck = GetComponent<BoundsCheck>(); //Bounds Check 
        _bndsCheck.keepOnScreen = false;
    }

    //increase the sheild level if it collides with the hero
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Hero" && col!=null)
        {
            col.gameObject.GetComponent<Hero>().shieldLevel += 1;
            Destroy(gameObject);
            SoundManager.soundManager.PowerUpSound();
        }
    }

    void Update()
    {
        if (!_bndsCheck.isOnScreen) Destroy(gameObject);
    }
}
