using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 :  Enemy
{
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

    // Update is called once per frame
    void Update()
    {
        this.Move();
        isOff();
    }
}
