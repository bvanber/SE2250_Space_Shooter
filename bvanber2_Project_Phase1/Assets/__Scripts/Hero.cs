using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    static public Hero S;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [Header("Set Dynamically")]
    public float shieldLevel = 1;

    void Awake()
    {
        if (S == null){
            S = this;
        }
        else{
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.s!");
        }
    }
	
	// Update is called once per frame
	void Update () {
        float _xAxis = Input.GetAxis("Horizontal");
        float _yAxis = Input.GetAxis("Vertical");

         Vector3 _pos = transform.position;
        _pos.x += _xAxis * speed * Time.deltaTime;
        _pos.y += _yAxis * speed * Time.deltaTime;
        transform.position = _pos;

        transform.rotation = Quaternion.Euler(_yAxis * pitchMult, _xAxis * rollMult, 0);

        Vector3 _pos2 = Camera.main.WorldToViewportPoint(transform.position);
        _pos2.x = Mathf.Clamp(_pos2.x,0.035f,0.965f);
        _pos2.y = Mathf.Clamp(_pos2.y,0.035f,0.965f);
        transform.position = Camera.main.ViewportToWorldPoint(_pos2);
    }
}
