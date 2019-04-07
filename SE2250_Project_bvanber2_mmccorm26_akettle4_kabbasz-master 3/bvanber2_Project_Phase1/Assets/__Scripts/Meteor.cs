using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //define class properties 
    protected BoundsCheck bndCheck;
    private float _speed;
    private float _horizontalVelocity;

    //define position as a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    //Move method runs every frame, based on speed and x velocity set Awake()
    public void Move()
    {
        Vector3 _tempPos = pos;
        _tempPos.y -= _speed * Time.deltaTime;
        _tempPos.x -= _horizontalVelocity * Time.deltaTime;
        pos = _tempPos;
    }
    //Awake method sets bounds check as well as both private properties used in Move()
    public void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        bndCheck.keepOnScreen = false;
        _horizontalVelocity = Random.Range(-3.5f, 3.5f);
        _speed = Random.Range(2.0f, 8.0f);
    }
    //IsOff method checks to see if this object is off the screen, if it is, it is destroyed and removed from the enemy list
    protected void IsOff()
    {
        if (bndCheck != null && !bndCheck.isOnScreen)
        {
            Main.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    //OnTriggerEnter method for when a collision is detected
    public  void OnTriggerEnter(Collider col) 
    {      
        //get the root of the object that collieded with this object (in case it is a compound object)
        GameObject otherObject = col.gameObject;
        Transform rootT = col.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        //if the object exists, check to see what action should take place
        if (otherObject != null)
        {
            //if the object is a black hole, destroy this object
            if (go.tag == "BlackHole")
            {
                Main.enemies.Remove(gameObject);
                Destroy(gameObject);
            }
            //if the object is another meteor, destroy the smaller one (or both if they are the same size)
            else if (go.tag == "Meteor")
            {
                if (gameObject.transform.localScale.x <= go.transform.localScale.x)
                {
                    Main.enemies.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
            //if the object is anything else aside from hero, destroy that object (the hero-meteor collision is handeled in the class Hero)
            //powerups are also not destroyed by meteors
            else if (go.tag != "Hero" && go.tag!= "PowerUp")
            {
                Destroy(go);
            }
        }
    }
    //move the meteor and check if it is off screne each frame using Update
    void Update()
    {
        Move();
        IsOff();
    }
}