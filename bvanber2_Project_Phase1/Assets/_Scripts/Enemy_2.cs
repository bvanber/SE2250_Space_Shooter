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

        pos = tempPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        
    }
}
