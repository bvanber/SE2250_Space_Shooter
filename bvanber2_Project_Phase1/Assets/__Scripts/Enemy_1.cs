﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy_1 : Enemy
{
    private static Random _rand = new Random();        //Random numbers 1 or 2 to determine diagonal movement
    private int _randomNum = _rand.Next(0, 2);
   
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

    // Update is called once per frame
    void Update()
    {

        this.Move();
        
    }
}