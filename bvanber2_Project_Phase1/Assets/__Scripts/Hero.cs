using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour 
{
    static public Hero ship;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    private GameObject lastTriggerGo = null;

    void Awake() //checks if ship is null to avoid null reference exception
    {
        if (ship == null)
        {
            ship = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.s!");
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        float _xAxis = Input.GetAxis("Horizontal");
        float _yAxis = Input.GetAxis("Vertical");

         Vector3 _pos = transform.position;
        _pos.x += _xAxis * speed * Time.deltaTime;
        _pos.y += _yAxis * speed * Time.deltaTime;
        transform.position = _pos;

        transform.rotation = Quaternion.Euler(_yAxis * pitchMult, _xAxis * rollMult, 0);

        Vector3 _pos2 = Camera.main.WorldToViewportPoint(transform.position);
        _pos2.x = Mathf.Clamp(_pos2.x, 0.035f, 0.965f);
        _pos2.y = Mathf.Clamp(_pos2.y, 0.035f, 0.965f);
        transform.position = Camera.main.ViewportToWorldPoint(_pos2);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go != null)
        {
            if (go == lastTriggerGo)
            {
                return;
            }
            lastTriggerGo = go;

            if (go.tag == "Enemy")
            {
                shieldLevel--;
                Destroy(go);
            }
            else
            {
                print("Triggered by non-Enemy: " + go.name);
            }
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4); //forces shield level to be no higher than 4
            //if the shield is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject); //destroys ship
                Main.scene.DelayedRestart(gameRestartDelay); //calls restart from Main with delay parameter
            }
        }
    }
}
