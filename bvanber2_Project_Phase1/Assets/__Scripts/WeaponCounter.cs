using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCounter : MonoBehaviour
{
    public Text SimpleWeaponCountText;                  //Defining texts to manipulate and display how many weapons are available of each type
    public Text BlasterWeaponCountText;
    public Text SurroundWeaponCountText;
    public Text SwivelWeaponCountText;
    public Text AnnihilateWeaponCountText;
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

    void Start()
    {
        //set the colours of the text fields to the colours of respective the weapon type colours
        SimpleWeaponCountText.color = new Color(47/255f, 241/255f, 206/255f);
        BlasterWeaponCountText.color = new Color(250/255f, 32/255f, 213/255f);
        SurroundWeaponCountText.color = new Color(248/255f, 8/255f, 8/255f);
        SwivelWeaponCountText.color = new Color(253/255f, 175/255f, 20/255f);
        AnnihilateWeaponCountText.color = new Color(241/255f, 241/255f, 208/255f);
    }

    void Update()
    {
        UpdateWeaponCount();
        //outputs number of weapons available to the game pane
        SimpleWeaponCountText.text = "x " + simpleCount.ToString();
        BlasterWeaponCountText.text = "x " + blasterCount.ToString();
        SurroundWeaponCountText.text = "x " + surroundCount.ToString();
        SwivelWeaponCountText.text = "x " + swivelCount.ToString();
        AnnihilateWeaponCountText.text= "x " + annihilateCount.ToString();
    }
}