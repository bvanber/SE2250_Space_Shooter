using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main scene;

    public GameObject[] prefabEnemies = new GameObject[3];
    public static List<GameObject> enemies = new List<GameObject>();
    public GameObject meteor;
    public GameObject blackHole;
    private float _timer;
    private float _timer2;
    private float _timer3;
    private Vector3 _startPos;
    private Vector3 _meteorPos;
    private Vector3 _blackHolePos;
    public WeaponDefinition[] weaponDefinitions;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
   
    void Awake()
    {
        if (scene == null) //checks if scene is null
        {
            scene = this;
        }
        else
        {
            Debug.LogError("Main.Awake()!");
        }
        
        //add weapons to the list
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition definition in weaponDefinitions)
        {
            WEAP_DICT[definition.type] = definition;
        }
    }
    
    //get the existing weapon with the desired type
    static public WeaponDefinition getWeaponDefinition(WeaponType tp)
    {
        if (WEAP_DICT.ContainsKey(tp))
        {
            return WEAP_DICT[tp];
        }
        //the right weapon wasnt found, return a new one with default none
        else return new WeaponDefinition();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = 1f;
        _timer2 = 1f;
        SoundManager.soundManager.GameStartSound();
    }

    // Update is called once per frame
    void Update()
    {
        System.Random rand = new System.Random();
        _timer -= Time.deltaTime;
        //when the timer reaches 0 time to add a new enemy
        if (_timer <= 0f)
        {
            //get a random number out of 3 for the enemy
            int num = rand.Next()%3;
            //if enemy 0 or 1 start above the view with a random x value otherwise start with a random y value if enemy 2
            if (num == 0 || num == 1) { _startPos = new Vector3(Random.Range(-25, 25), 40, 0); }
            else { _startPos = new Vector3(30, Random.Range(0, 55), 0); }

            enemies.Add(Instantiate(prefabEnemies[(num) % 3],_startPos, Quaternion.identity));
            _timer = 2f;//reset the timer
        }

        //decrament timer2, when timer2 reaches 0, spawn a meteor
        _timer2 -= Time.deltaTime;
        if ((_timer2 <= 0f)&&((LevelManager.GetLevel() == 2)|| (LevelManager.GetLevel() == 3)))
        {
            //get a random stsrting x position for the meteor
            _meteorPos = new Vector3(Random.Range(-25, 25), 40, 0);
            GameObject go = Instantiate(meteor, _meteorPos, Quaternion.identity);
            enemies.Add(go);
            go.transform.localScale = go.transform.localScale * Random.Range(2.0f, 8.0f);
            _timer2 = 4f;//reset the timer
        }

        _timer3 -= Time.deltaTime;
        if ((_timer3 <= 0f) && (LevelManager.GetLevel() == 3))
        {
            //get a random stsrting x position for the meteor
            _blackHolePos = new Vector3(Random.Range(-25, 25), 40, 0);
            Instantiate(blackHole, _blackHolePos, Quaternion.identity);
            _timer3 = 30f;//reset the timer
        }

    }
   
    public void DelayedRestart(float delay)
    {
        //invoked the Restart() method in delay seconds
        Invoke("Restart", delay);
        
    }

    public void Restart()
    {
        //reload scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ScoreManager.ResetScore();
        LevelManager.ResetLevel();
        WeaponCounter.ResetCount();
        Enemy.ResetAllFreq();
    }
}
