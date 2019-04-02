using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour      //Superclass from which the other 2 Enemy scripts inherit
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 0.5f;           //Defining variables
    public float health = 1;            //health for enemy resilience
    public int points = 100;
    protected BoundsCheck bndCheck;
    public GameObject powerUp;
    private static int _dropPowerUp;
    protected int powerUpFreq = 6;
    public delegate void WeaponFireDelegate(); //delegate for enemies that shoot
    public WeaponFireDelegate fireDelegate;

    //property for the powerup counter for the base class
    public virtual int PowerUpCounter
    {
        get{
            return _dropPowerUp;
        }
        set
        {
             _dropPowerUp= value;
        }
    }

    //empty fire function for enemies that dont shoot
    void fire()
    {

    }

    public void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>(); //Bounds Check 
        bndCheck.keepOnScreen = false;
        fireDelegate +=fire;//default shooting is no shooting
        _dropPowerUp = powerUpFreq;//initialize the powerup frequency
    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }                                             //Defining field pos
        set
        {
            this.transform.position = value;
        }
    }

    public virtual void Move()
    {
        Vector3 _tempPos = pos;       
        _tempPos.y -= speed * Time.deltaTime;      //Creating virtual function that can e overriden by the subclass of Enemy       
        pos = _tempPos;
        
    }
    //bounds checking
    protected void IsOff()
    {
        if (bndCheck != null && !bndCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
    }

    //Allow the bullets to destroy the enemy
    public virtual void OnTriggerEnter(Collider col) //Making function virtual so that different amount of points can be added to
    {                                                //score depending on the destroyed enemy
        GameObject otherObject = col.gameObject;
        if (otherObject != null)//in case its already been destroyed
        {
            if (otherObject.tag == "ProjectileHero")//if the collision is from a hero bullet destroy both objects
            {
                fireDelegate(); //When enemy is hit by the hero, fire projectile 
                health--;           
                if (health <= 0)
                {
                    ScoreManager.ScoreIncrease(points);    //Calling function to increase score 
                    if (PowerUpCounter > 0)
                    {
                        PowerUpCounter = PowerUpCounter - 1; ;
                    }
                    else
                    {
                        PowerUpCounter = powerUpFreq;
                        DropPowerUp();
                    }
                    Main.enemies.Remove(gameObject);
                    Destroy(gameObject);
                }
                Destroy(otherObject);
            }
            if (otherObject.tag == "BlackHole")
            {
                print("enn 0 black hole");
                Main.enemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }
        
        
    }

    //dropping a powerup 
    public virtual void DropPowerUp()
    {
        GameObject drop = Instantiate(powerUp);
        drop.gameObject.transform.position = gameObject.transform.position;
        drop.GetComponent<Rigidbody>().velocity = Vector3.down * 10;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        IsOff();
    }
}
