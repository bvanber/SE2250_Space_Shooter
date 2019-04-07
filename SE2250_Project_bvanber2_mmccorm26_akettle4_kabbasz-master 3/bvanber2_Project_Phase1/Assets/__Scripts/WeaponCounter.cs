using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCounter : MonoBehaviour
{
    public Text weaponCountText;                  //Defining texts to manipulate and display how many weapons are available of each type
    public static int simpleCount = 100;          //Player starts with 100 simple bullets, 50 blaster bullets, 20 surround bullets, 10 swivel bullets, and 5 annihilate bullets
    public static int blasterCount = 50;
    public static int surroundCount = 20;
    public static int swivelCount = 10;
    public static int annihilateCount = 5;
    public static int scoreCount = 0;

    //externally called to decrement number of each type of shot available
    public static void DecrementSimple() 
    {
        simpleCount--;
    }

    public static void DecrementBlaster()
    {
        blasterCount--;
    }

    public static void DecrementSurround()
    {
        surroundCount--;
    }

    public static void DecrementSwivel()
    {
        swivelCount--;
    }

    public static void DecrementAnnihilate()
    {
        annihilateCount--;
    }

    public static void UpdateWeaponCount()  
    {
        if (scoreCount >= 2000) //resets weapon count everytime the player gets 2000 points
        {
            ResetCount();
            scoreCount = 0;
        }
    }

    public static void ResetCount() //resets to original weapon count values
    {
        simpleCount = 100;
        blasterCount = 50;
        surroundCount = 20;
        swivelCount = 10;
        annihilateCount = 5;
    }
    void Update()
    {
        UpdateWeaponCount();
        //outputs number of weapons available to the game pane
        weaponCountText.text = "x " + simpleCount.ToString() + "\nx " + blasterCount.ToString() + "\nx " + surroundCount.ToString() + "\nx " + swivelCount.ToString() + "\nx " + annihilateCount.ToString();
    }
}