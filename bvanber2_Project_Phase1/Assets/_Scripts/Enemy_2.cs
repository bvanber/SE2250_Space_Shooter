using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 :  Enemy
{
    override public void Move()
    {
        Vector3 tempPos = pos;
        gameObject.transform.Rotate(0,0,5);

        tempPos.y -= speed * Time.deltaTime;

        tempPos.x -= speed * Time.deltaTime*Random.Range(0.5f,1f);
     
        pos = tempPos;
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        
    }
}
