using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 :  Enemy
{
    public int points2 = 300;
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
            health--;

            ScoreManager.ScoreIncrease(points2); //Calling function to increase score 
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
        this.Move();
        isOff();
    }
}
