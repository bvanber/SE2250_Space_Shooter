using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Set Dynamically")]
    private BoundsCheck _bndsCheck;
    public Renderer rend;
    public Rigidbody rigid;
       

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
        _bndsCheck = gameObject.GetComponent<BoundsCheck>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "BlackHole") Destroy(gameObject);
    }
   
    // Update is called once per frame
    void Update()
    {
        //destroy if the bullet has gone off screen
        if (_bndsCheck!=null && !_bndsCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
    }
}
