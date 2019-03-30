using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy_1 : Enemy
{
    
    private static Random _rand = new Random();        //Random numbers 1 or 2 to determine diagonal movement
    private int _randomNum = _rand.Next(0, 2);
    public int points1 = 200;
    static private int _dropUpShield = 3; //drops a Shield powerup every 3 kills of this enemy
   
    // Start is called before the first frame update
    void Start()
    {
        if(_randomNum == 0)
        {
            gameObject.transform.Rotate(0, 0, -45);     //Rotating Enemy_1 for more appealing diagonal movement
        }
        else
        {
            gameObject.transform.Rotate(0, 0, 45);
        }
        health = 2;        
    }

    override public void Move()
    {
        Vector3 _tempPos = pos;
        if (_randomNum == 0)
        {
            
            _tempPos.y -= speed * Time.deltaTime;
            _tempPos.x -= speed * Time.deltaTime;       //Making Enemy_1 move down left or right randomly
            pos = _tempPos;
        }
        else
        {
            _tempPos.y -= speed * Time.deltaTime;
            _tempPos.x += speed * Time.deltaTime;
            pos = _tempPos;

        }
    }

    public override void OnTriggerEnter(Collider col)
    {
        GameObject otherObject = col.gameObject;
        if (otherObject.tag == "ProjectileHero")//if the collision is from a hero bullet destroy both objects
        {          
            health--;            
            if (health <= 0)
            {
                ScoreManager.ScoreIncrease(points1); //Calling function to increase score 
                //is it time to drop a powerup
                if (_dropUpShield > 0) _dropUpShield--;
                else
                {
                    _dropUpShield = 3;
                    DropPowerUp();
                }
                Destroy(gameObject);
            }
            Destroy(otherObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        IsOff();
    }
}
