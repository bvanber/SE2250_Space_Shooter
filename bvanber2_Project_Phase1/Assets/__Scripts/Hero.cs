﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour 
{
    static public Hero ship;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    //fields relating to weapons
    public GameObject projectile;
    public WeaponType weaponType;
    public float projectileSpeed = 1f;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    private GameObject _lastTriggerGo = null;
    public bool isInvincible = false;

    void Awake() //checks if ship is null to avoid null reference exception
    {
        if (ship == null)
        {
            ship = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.s!");
        }
        //assign the delegate
        Weapon weapon = transform.Find("Weapon").gameObject.GetComponent <Weapon> ();
        weaponType=WeaponType.simple;//This changes the weapon type, use these
        weapon.setType(weaponType);//two lines when the weapon type changes
        fireDelegate += weapon.Fire;
        gameObject.GetComponent<FlashingColourManager>().status = Status.off;
    }

    // Update is called once per frame
    void Update()
    {
        float _xAxis = Input.GetAxis("Horizontal");
        float _yAxis = Input.GetAxis("Vertical");

        Vector3 _pos = transform.position;
        _pos.x += _xAxis * speed * Time.deltaTime;
        _pos.y += _yAxis * speed * Time.deltaTime;
        transform.position = _pos;

        transform.rotation = Quaternion.Euler(_yAxis * pitchMult, _xAxis * rollMult, 0);

        //Allow the ship to shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireDelegate();
        }

        //switch between weapons when key is pressed
        Weapon weapon = transform.Find("Weapon").gameObject.GetComponent<Weapon>();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            weapon.setType(WeaponType.simple);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            weapon.setType(WeaponType.blaster);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            weapon.setType(WeaponType.surround);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            weapon.setType(WeaponType.swivel);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            weapon.setType(WeaponType.annihilate);
    }
   
 
    private void OnTriggerEnter(Collider other)
    {

        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
  
        if (go != null)//in case its already been destroyed
        {
            if (go == _lastTriggerGo)
            {
                return;
            }
            _lastTriggerGo = go;
            //check to see what triggered the collision
            if (go.tag == "Enemy"&& !isInvincible)
            {
                shieldLevel--;
                Destroy(go);
            }
            else if(go.tag == "ProjectileEnemy" && !isInvincible)
            {
                shieldLevel--;
                Destroy(go);
            }
            else if(go.tag == "Meteor" && !isInvincible)
            {
                shieldLevel = -1;
            }
            else if (go.tag == "BlackHole")
            {
                shieldLevel = -1;
            }
            else
            {
                print("Triggered by non-Enemy: " + go.name);
                return; 
            }
        }
        SoundManager.soundManager.CrashSound(); //When hero is hit call crashSound function
    }
    //property for the shield
    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4); //forces shield level to be no higher than 4
            //if the shield is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject); //destroys ship
                Main.scene.DelayedRestart(gameRestartDelay); //calls restart from Main with delay parameter
            }
        }
    }
}
