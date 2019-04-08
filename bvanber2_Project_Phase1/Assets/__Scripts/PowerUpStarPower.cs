using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStarPower : MonoBehaviour
{
    private BoundsCheck _bndsCheck;
    private float _timeLength=80;
    private GameObject _ship;
    public FlashingColourManager shipFlashy;
    private bool _hasCollided;

    void Start()
    {
        _bndsCheck = GetComponent<BoundsCheck>(); //Bounds Check 
        _bndsCheck.keepOnScreen = false;
    }

    void OnTriggerEnter(Collider col)
    {
        //only reacts if it collides with hero
        if (col.gameObject.tag == "Hero")
        {
            _ship = col.gameObject;//initialize ship field as we will be using it now
            _ship.GetComponent<Hero>().isInvincible = true;
            //move the powerup off the screen and stop its velocity while its effects are active
            gameObject.transform.position = new Vector3(50, 0, 0);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //we need the FlashingColorManager for the ship so we can change the status later
            shipFlashy = _ship.GetComponent<FlashingColourManager>();
            shipFlashy.status=Status.on;
            _hasCollided = true;
            SoundManager.soundManager.PowerUpSound();
        }
    }

    void Update()
    {
        if (_bndsCheck.offDown) Destroy(gameObject);//if the powerup is never picked up
        //When the powerup has been picked up its invincible and flashing colors
        if (_hasCollided && _timeLength > 20)
        {
            _timeLength -= 0.1f;

        }
        //When we get close to the end of the time the ship is still invincible but the color flashing fades
        else if(_hasCollided && _timeLength <=20 && _timeLength>0 && _ship!=null)
        {
            shipFlashy.status = Status.fade;
            _timeLength -= 0.1f;
        }
        //We reach the end of the time, reset colors, make ship mortal again, destroy powerup
        else if (_hasCollided && _timeLength <= 0 && _ship!=null) 
        {
            shipFlashy.status = Status.off;
            shipFlashy.ReturnToDefault();
            _ship.GetComponent<Hero>().isInvincible = false;
            Destroy(gameObject);
        }

    }
}
