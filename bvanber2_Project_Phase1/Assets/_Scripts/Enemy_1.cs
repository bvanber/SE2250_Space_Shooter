using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy_1 : Enemy
{
    private static Random _rand = new Random();
    private int _randomNum = _rand.Next(0, 2);
   


    // Start is called before the first frame update
    void Start()
    {
        if(_randomNum == 0)
        {
            gameObject.transform.Rotate(0, 0, -45);
        }
        else
        {
            gameObject.transform.Rotate(0, 0, 45);
        }
        
        
    }
    override public void Move()
    {
        Vector3 tempPos = pos;
        if (_randomNum == 0)
        {
            
            tempPos.y -= speed * Time.deltaTime;
            tempPos.x -= speed * Time.deltaTime;
            pos = tempPos;
        }
        else
        {
            tempPos.y -= speed * Time.deltaTime;
            tempPos.x += speed * Time.deltaTime;
            pos = tempPos;

        }

    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        
    }
}
