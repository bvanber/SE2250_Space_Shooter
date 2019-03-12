using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main scene;

    public GameObject[] prefabEnemies = new GameObject[3];
    private float _timer;
    private Vector3 _startPos;
    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _toDelete = new List<GameObject>();

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
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = 1f;
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
            if (num == 0 || num == 1) { _startPos = new Vector3(Random.Range(-25, 25), 45, 0); }
            else { _startPos = new Vector3(40, Random.Range(-20, 55), 0); }

            GameObject enn= Instantiate(prefabEnemies[(num) % 3],_startPos, Quaternion.identity);
            _enemies.Add(enn);
            _timer = 2f;//reset the timer
        }


        foreach(GameObject dooDad in _enemies)
        {
            //check if the enemies are out of view
            if (dooDad == null || dooDad.transform.position.x<-35|| dooDad.transform.position.x > 41|| dooDad.transform.position.y < -50|| dooDad.transform.position.x > 70)
            {
                _toDelete.Add(dooDad);
            }
        }


        //delete the out of view enemies
        foreach (GameObject bye in _toDelete)
        {
            _enemies.Remove(bye);
            Destroy(bye);
        }
        _toDelete.Clear();
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
    }
}
