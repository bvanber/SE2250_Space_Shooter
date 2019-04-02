using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    //define nessesary parameters

    //distance where gravity effect works, gravity strength, black hole speed
    public float maxGravDistance = 20.0f;
    public float maxGravity = 0.1f;
    public float speed = 2.5f;
    //boundscheck instance
    protected BoundsCheck bndCheck;
    //properties for objects to attract
    private Rigidbody _ship;
    public List<Rigidbody> enemies;
    private GameObject _heroGO;
    //proporties for particle effect on black hole
    public GameObject particle;
    private List<GameObject> _particles = new List<GameObject>();
    private List<GameObject> _toDelete = new List<GameObject>();
    public float sclaingFactor = 0.5f;

    //define position as a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    //on Start, get a reference to the hero ship
    public void Start()
    {
        if (Hero.ship!=null)_heroGO = Hero.ship.gameObject;
        
    }
    
    public void OnTriggerEnter(Collider col)
    {
        GameObject otherObject = col.gameObject;
        Transform rootT = col.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (otherObject != null)
        {
            if (go.tag != "Hero" && go.tag !="PowerUp" && go.tag!="Meteor" && go.tag!="Enemy")
            {
                Destroy(go);
            }
        }
    }

    //Move function moves black hole directly down the screen, changing y position each update
    public void Move()
    {
        Vector3 _tempPos = pos;
        _tempPos.y -= speed * Time.deltaTime; 
        pos = _tempPos;
    }

    //IsOff method to check if the black hole is off the screen, if it is, destroy it
    protected void IsOff()
    {
        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }

    //Update method is used for the creation of particles
    public void Update()
    { 
        //call the make particles function twice, to create two particles each with a random velocity
        MakeParticle();
        MakeParticle();
        //for each particle that had been created, scale it down by the scaling factor each update until it is small enough, it is then added to the delete list
        foreach (GameObject p in _particles)
        {
            p.transform.localScale *= sclaingFactor;
            p.transform.RotateAround(transform.position, Vector3.back, 300 * Time.deltaTime);
            if (p.transform.localScale.x <= 0.005f) _toDelete.Add(p);
        }
        //for each particle in the delete list, remove it from the particles list and destroy it
        foreach (GameObject d in _toDelete)
        {
            _particles.Remove(d);
            Destroy(d);
        }
        _toDelete.Clear();
    }

    //MakeParticle method created one particle, assigns it a random velocity, and adds it to the particles list
    private void MakeParticle()
    {
        GameObject obj = Instantiate(particle);
        obj.transform.position = transform.position + new Vector3(0, 0, 2);
        obj.GetComponent<Rigidbody>().velocity = Random.insideUnitCircle;
        _particles.Add(obj);
    }

    //FixedUpdate is used to translate objects bing attracted so that the transations in the update function in other classes is not interfeared with
    public void FixedUpdate()
    {
        //if there exists a hero currently, run the gravity effect on the hero
        if (_heroGO != null)
        {
            GravityFX(Hero.ship.gameObject);
        }
        //for each enemy(including meteors) apply the gravity effect to that enemy
        foreach (GameObject go in Main.enemies)
        {
            GravityFX(go);
        }
        //move the black hole and check if the black hole needs to be deleted
        Move();
        IsOff();

    }
    
    //GravityFX function handles the gravity effect on other gameobjects
    private void GravityFX(GameObject go)
    {
        //if the passed game ogject curerently exists, execute the function
        if (go != null)
        {
            //calculate the distance between the black hole and the game object
            float distance = Mathf.Abs(Vector3.Distance(go.transform.position, this.transform.position));
            //if the distance is very very small, set it to 0.05 so that later, it is never possible we will have to divide by 0
            if (distance <= 0.05f) distance = 0.05f;
            //if the object is inside the gravity effect radius, apply the gravity effect
            if (distance <= maxGravDistance)
            {
                //utilize the MoveTowards function to move the object towards the black hole
                //scale the movement by multiplying it by the grvity constant and 1/distance^2 to simulate real gravity
                go.transform.position = Vector3.MoveTowards(go.gameObject.transform.position, transform.position, maxGravity * (1.0f / distance)*(1.0f/distance));
            }
        }
    }
}