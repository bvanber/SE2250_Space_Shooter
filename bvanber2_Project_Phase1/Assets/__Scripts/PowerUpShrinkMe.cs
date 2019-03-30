using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShrinkMe : MonoBehaviour
{
    private float _shrinkTime=0.5f;
    private GameObject _ship;
    private bool _hasCollided;//field to keep track of whethter the powerup has hit the ship
    private Vector3 _origScale;
    private BoundsCheck _bndsCheck;

    // Start is called before the first frame update
    void Start()
    {
        _hasCollided = false;
        _bndsCheck = GetComponent<BoundsCheck>(); //Bounds Check 
        _bndsCheck.keepOnScreen = false;
    }

    void OnTriggerEnter(Collider col)
    {
        //if powerup hits ship
        if (col.gameObject.tag == "Hero")
        {
            _hasCollided = true;
            _ship = col.gameObject;//get the ship game object so we can access it later
            //move the power up off to the side while its effects are active
            gameObject.transform.position = new Vector3(55, 3, 0);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _origScale = _ship.transform.localScale;
            _ship.transform.localScale = new Vector3(_shrinkTime, _shrinkTime, _shrinkTime);
        }
    }

    //to put the ship back to its original scale
    private void ResetScale()
    {
        _ship.transform.localScale = _origScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bndsCheck.offDown) Destroy(gameObject);//if the gameobject isnt picked up

        //the powerup pickup is constantly shrinking and growing
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f) * (0.3f*Mathf.Sin(2*Time.time)+0.7f);

        //upon collision, the ship slowly regrows now
        if (_hasCollided && _shrinkTime<=1f &&_ship!=null)
        {
            _shrinkTime += 0.001f;
            _ship.transform.localScale = new Vector3(_shrinkTime, _shrinkTime, _shrinkTime);
        }
        //end of powerup, return ship to original size and destroy powerup
        if (_hasCollided && _shrinkTime >= 1 && _ship!=null)
        {
            ResetScale();
            Destroy(gameObject);
        }
    }
}
