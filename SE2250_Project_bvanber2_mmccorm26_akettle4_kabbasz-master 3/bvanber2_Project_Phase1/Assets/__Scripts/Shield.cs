﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationPerSecond = 0.1f;

    [Header("Set Dynamically")]
    public int levelShown = 0;

    //non-public variable will not appear in the inspector
    Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        //read the current shield level from the Hero Singleton
        int currLevel = Mathf.FloorToInt(Hero.ship.shieldLevel);
        //if this is different from levelShown ...
        if (levelShown != currLevel)
        {
            levelShown = currLevel;
            //adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0); 
        }
        //rotate the shield a bit every frame in a time-based way
        float rZ = -(rotationPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
