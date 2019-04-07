using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text levelText;                  //Defining texts to manipulate and display score and high score
    private static int _level = 1;

    public static void UpdateLevel()
    {
        if ((_level == 1)&&(ScoreManager.GetScore()>= 2000))
        {
            _level = 2;
            SoundManager.soundManager.LevelUpSound();
        }
        if ((_level!= 3) && (ScoreManager.GetScore() >= 4000)) //If a certain score is reached, level up
        {
            _level = 3;
            SoundManager.soundManager.LevelUpSound();
        }
    }

    public static int GetLevel()
    {                                   
        return _level;                  //Get the current level
    }

    public static void ResetLevel()         
    {
        _level = 1;                     //Reset Level back to 1
    }
    void Update()
    {
        UpdateLevel();                                     //Display Level
        levelText.text = "Level: " + _level.ToString();
    }
}
