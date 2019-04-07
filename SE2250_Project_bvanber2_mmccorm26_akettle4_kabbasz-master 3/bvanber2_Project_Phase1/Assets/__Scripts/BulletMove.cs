using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float MoveSpeed = 5.0f;
    public float frequency = 20.0f;  // Speed of sine movement
    public float magnitude = 0.5f;   // Size of sine movement
    private Vector3 axis;
    private Vector3 pos;
    void Start()
    {
        Weapon weapon = transform.Find("Weapon").gameObject.GetComponent<Weapon>(); 
        pos = transform.position;
        Object.Destroy(gameObject);
        axis = transform.right;  
    }

    void FixedUpdate()
    {
        pos += transform.up * Time.deltaTime * MoveSpeed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
