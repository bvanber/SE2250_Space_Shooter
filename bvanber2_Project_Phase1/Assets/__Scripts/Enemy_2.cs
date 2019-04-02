using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 :  Enemy
{
    public GameObject projectile;
    public WeaponType weaponType;
    public float projectileSpeed = 1f;
    public Weapon weapon;
    private static int _dropStar = 3;//drops a starPower Powerup every 3 kills of this enemy

    void Start()
    {
        health = 3;
        this.powerUpFreq = 3;
        points = 300;
        //assign the delegate
        Weapon weapon = transform.Find("Weapon").gameObject.GetComponent<Weapon>();
        weaponType = WeaponType.enemy;//This changes the weapon type, use these
        weapon.setType(weaponType);//two lines when the weapon type changes
        fireDelegate += weapon.Fire;
    }
    //property for the power up counter for this subclass
    public override int PowerUpCounter
    {
        get
        {
            return _dropStar;
        }
        set
        {
            _dropStar = value;
        }
    }

    override public void Move()
    {
        Vector3 _tempPos = pos;
        gameObject.transform.Rotate(0,0,-3);

        _tempPos.y -= speed * Time.deltaTime;

        _tempPos.x -= speed * Time.deltaTime*Random.Range(0.5f,1f);
     
        pos = _tempPos;
    }

    //need to override base method for dropping this powerup since theres more to do to initialize it
    public override void DropPowerUp()
    {
        GameObject drop = Instantiate(powerUp);
        drop.GetComponent<FlashingColourManager>().status = Status.on;
        drop.gameObject.transform.position = gameObject.transform.position;
        drop.GetComponent<Rigidbody>().velocity = Vector3.down * 10;
    }
}
