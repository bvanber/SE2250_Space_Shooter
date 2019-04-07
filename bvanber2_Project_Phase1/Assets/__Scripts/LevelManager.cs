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
        if ((_level ==1)&&(ScoreManager.GetScore()>= 2000))
        {
            _level = 2;
        }
        if ((_level!= 3) && (ScoreManager.GetScore() >= 4000))
        {
            _level = 3;
        }
    }

    public static int GetLevel()
    {
        return _level;
    }

    public static void ResetLevel()
    {
        _level = 1;
    }
    void Update()
    {
        UpdateLevel();
        levelText.text = "Level: " + _level.ToString();
    }
}
