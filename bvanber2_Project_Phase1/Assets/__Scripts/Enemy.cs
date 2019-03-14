using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour      //Superclass from which the other 2 Enemy scripts inherit
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;           //Defining variables
    public float health = 1;            //health for enemy resilience
    protected BoundsCheck bndCheck;

    private void Awake()
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

    protected void isOff()
    {
        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }

    //Allow the bullets to destroy the enemy
    void OnTriggerEnter(Collider col)
    {
        GameObject otherObject = col.gameObject;
        if (otherObject.tag == "ProjectileHero")//if the collision is from a hero bullet destroy both objects
        {
            health--;
            if (health <= 0)
            {
                 Destroy(gameObject);
            }
           Destroy(otherObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        isOff();
    }
}
