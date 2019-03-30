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
    private static int _dropShrinker = 7;

    public void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>(); //Bounds Check 
        bndCheck.keepOnScreen = false;
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
       
        _tempPos.y -= speed * Time.deltaTime;       //Creating virtual function that can e overriden by the subclass of Enemy
       
        pos = _tempPos;
    }

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
                health--;           
                if (health <= 0)
                {
                    ScoreManager.ScoreIncrease(points);    //Calling function to increase score 
                    if (_dropShrinker > 0)
                    {
                        _dropShrinker--;
                    }
                    else
                    {
                        _dropShrinker = 3;
                        DropPowerUp();
                    }
                    Destroy(gameObject);
                }
                Destroy(otherObject);
            }
        }
        
        
    }

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
