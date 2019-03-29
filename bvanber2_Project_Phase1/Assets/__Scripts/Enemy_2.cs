using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 :  Enemy
{
    public GameObject projectile;
    public WeaponType weaponType;
    public float projectileSpeed = 1f;
    public delegate void WeaponFireDelegate(); //Adding weapon functionality to Enemy_2
    public WeaponFireDelegate fireDelegate;
    public int points2 = 300;
    public int shootcCount = 0;
    public Weapon weapon;
    void Awake()
    {
        //assign the delegate
        Weapon weapon = transform.Find("Weapon").gameObject.GetComponent<Weapon>();
       
        weaponType = WeaponType.enemy;//This changes the weapon type, use these
        weapon.setType(weaponType);//two lines when the weapon type changes
        fireDelegate += weapon.Fire;
    }

    void Start()
    {
        health = 3;
    }

    override public void Move()
    {
        Vector3 _tempPos = pos;
        gameObject.transform.Rotate(0,0,5);

        _tempPos.y -= speed * Time.deltaTime;

        _tempPos.x -= speed * Time.deltaTime*Random.Range(0.5f,1f);
     
        pos = _tempPos;
    }
    public override void OnTriggerEnter(Collider col)
    {
        GameObject otherObject = col.gameObject;
        if (otherObject.tag == "ProjectileHero")//if the collision is from a hero bullet destroy both objects
        {
            fireDelegate(); //When enemy is hit by the hero, fire projectile 
            health--;
            
            
            if (health <= 0)
            {
                ScoreManager.ScoreIncrease(points2); //Calling function to increase score 
                Destroy(gameObject);
            }
            Destroy(otherObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.Move();
        isOff();
    }
}
