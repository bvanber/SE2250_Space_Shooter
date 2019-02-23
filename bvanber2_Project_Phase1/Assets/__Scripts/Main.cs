using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{ 
    public GameObject[] prefabEnemies = new GameObject[3];
    private float _timer;
    private Vector3 _startPos;
    public List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _toDelete = new List<GameObject>();
   
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
            if (num == 0 || num == 1) { _startPos = new Vector3(Random.Range(-45, 45), 60, 0); }
            else { _startPos = new Vector3(50, Random.Range(-45, 60), 0); }

            GameObject enn= Instantiate(prefabEnemies[(num) % 3],_startPos, Quaternion.identity);
            _enemies.Add(enn);
            _timer = 2.5f;//reset the timer
        }

        foreach(GameObject dooDad in _enemies)
        {
            //check if the enemies are out of view
            if (dooDad.transform.position.x<-60|| dooDad.transform.position.x > 60|| dooDad.transform.position.y < -60|| dooDad.transform.position.x > 80)
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
}
