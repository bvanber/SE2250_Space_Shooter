using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;                  //Defining texts to manipulate and display score and high score
    private static int _high_score = 0;         //Defining necessary variables
    private static int _score = 0;

    public static void ScoreIncrease(int points)        //Defining static function that increases the score 
    {
        _score += points;
        WeaponCounter.scoreCount += points;
    }
    public static void ResetScore()
    {
        _score = 0;
    }

    public static int GetScore()
    {
        return _score;
    }
   
    void Awake()
    {

        _high_score = PlayerPrefs.GetInt("Shump_high_score"); //Getting high score if there is a pre-existing high score in PlayerPrefs
    }                                                         
    void Update()
    {
        scoreText.text = "Score: "+_score.ToString();                   //Displaying Score and High Score
        highScoreText.text = "High Score: " + _high_score.ToString();
        if (_score > _high_score)
        {
            _high_score = _score;
            PlayerPrefs.SetInt("Shump_high_score", _score); //If score exceeds high score, new high score is set and placed in PlayerPrefs
        }
    }
        
}
