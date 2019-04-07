using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum WeaponType
    {
        none,
        simple,
        blaster,
        enemy, //adding weapon type for enemy
        surround, //weapon fires 8 bullets at a time
        swivel, //weapon aims to nearest enemy
        annihilate //weapon kills all enemies on screen
    }


[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.simple;
    public GameObject projectilePrefab;
    public Color projectileColour;
    public float velocity = 40f;
}


public class Weapon : MonoBehaviour
{
    [Header("Set Dynamically")]
    [SerializeField]
    public WeaponType _type;
    private WeaponDefinition _def;
    private GameObject collar;
    private Renderer _collarRend;
    private GameObject _nearestEnemy;

    private GameObject _swivelBullet=null;

    public void Fire()
    {
        Projectile p;
        Vector3 velocity = Vector3.up * _def.velocity;
        if (transform.up.y < 0)
        {
            velocity.y = -velocity.y;
        }

        //fire based on which kind of weapon
        switch (_type)
        {
            case WeaponType.simple:
                if (WeaponCounter.simpleCount == 0) {;}
                else
                {
                    p = makeProjectile();
                    p.rigid.velocity = velocity;
                    WeaponCounter.DecrementSimple();
                }
                break;

            case WeaponType.blaster:
                if (WeaponCounter.blasterCount == 0) {;}
                else
                {
                    p = makeProjectile();//middle bullet
                    p.rigid.velocity = velocity;
                    p = makeProjectile();//right bullet
                    p.transform.rotation = Quaternion.AngleAxis(30, Vector3.back);
                    p.rigid.velocity = p.rigid.rotation * velocity;
                    p = makeProjectile();//left bullet
                    p.transform.rotation = Quaternion.AngleAxis(-30, Vector3.back);
                    p.rigid.velocity = p.rigid.rotation * velocity;
                    WeaponCounter.DecrementBlaster();
                }

                break;

            case WeaponType.enemy:
                p = makeProjectile();
                p.rigid.velocity = Vector3.down * _def.velocity;//Enemy weapon type shots straight down, not based on enemy orientation
                break;

            case WeaponType.surround: //surround weapon shoots out one bullet every 45 degrees, 8 bullets total
                if (WeaponCounter.surroundCount == 0) {;}
                else
                {
                    for (int i = 0; i < 360; i += 45)
                    {
                        p = makeProjectile();
                        p.transform.rotation = Quaternion.AngleAxis(i, Vector3.back);
                        p.rigid.velocity = p.rigid.rotation * velocity;
                    }
                    WeaponCounter.DecrementSurround();
                }

                break;

            case WeaponType.swivel: //aims to nearest enemy, shoots 3 bullets in that direction
                if (WeaponCounter.swivelCount == 0) {;}
                else if(_swivelBullet==null)
                {
                    p = makeProjectile();
                    _swivelBullet = p.gameObject;
                    WeaponCounter.DecrementSwivel();
                }
                break;

            case WeaponType.annihilate: //annihilates all enemies on screen by sending out one weapon every 2 degrees, 180 bullets total
                if (WeaponCounter.annihilateCount == 0) {;}
                else
                {
                    for (int i = 0; i < 360; i += 2)
                    {
                        p = makeProjectile();
                        p.transform.rotation = Quaternion.AngleAxis(i, Vector3.back);
                        p.rigid.velocity = p.rigid.rotation * velocity;
                    }
                    WeaponCounter.DecrementAnnihilate();
                }
                break;
        }
        SoundManager.soundManager.ShotSound(); //Whenever weapon fires call the shotSound function
    }

    public void Update() //called every frame 
    {
        _nearestEnemy = FindClosestEnemy(); 

        if (_swivelBullet!=null && _nearestEnemy != null) //checks if bullet and nearest enemy exist on screen
        {
            Vector3 target = _nearestEnemy.transform.position;

            if (_nearestEnemy.GetComponent<Enemy_2>()!=null)//enemy_2 requires a special case because of its movement pattern
            {
               target = target + new Vector3(4, 2, 0);
            }
            _swivelBullet.gameObject.transform.position = Vector3.MoveTowards(_swivelBullet.transform.position, target, 1f); //moves bullet toward closest enemy
        }
    }

    public GameObject FindClosestEnemy() //finds closest enemy using distance between hero and all enemies on screen
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    //make the projectile
    public Projectile makeProjectile()
    {
        GameObject bullet = Instantiate(_def.projectilePrefab);
       if (transform.parent.gameObject.tag == "Hero")
       {
            bullet.tag = "ProjectileHero";
            
       }
       if(transform.parent.gameObject.tag == "Enemy")//Tagging projectiles based on the gameobject
        {
            bullet.tag = "ProjectileEnemy";
        }
        //make the projectile originate from the end of the weapon
        bullet.transform.position = collar.transform.position;
        Projectile p = bullet.GetComponent<Projectile>();
        p.rend.material.color = _def.projectileColour;
        return p;
    }

    //this is first called in Hero Awake() so it sets up everything we need to use this class
    public void setType(WeaponType ty)
    {
        _type = ty;
        if (ty == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else this.gameObject.SetActive(true);
        _def = Main.getWeaponDefinition(ty);

        collar = transform.Find("Collar").gameObject;
        _collarRend = collar.GetComponent<Renderer>();
        //set the collar on the weapon to be the same colour as the projectile so you know which weapon you are using
        _collarRend.material.color = _def.projectileColour;
    }
    
}
