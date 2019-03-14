using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum WeaponType
    {
        none,
        simple,
        blaster
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
    private WeaponType _type;
    private WeaponDefinition _def;
    private GameObject collar;
    private Renderer _collarRend;

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
               // _collarRend.material.color = Main.getWeaponDefinition(_type).projectileColour;
                p = makeProjectile();
                p.rigid.velocity = velocity;
                break;
            case WeaponType.blaster:
               // _collarRend.material.color = Main.getWeaponDefinition(_type).projectileColour;
                p = makeProjectile();//middle bullet
                p.rigid.velocity = velocity;
                p = makeProjectile();//right bullet
                p.transform.rotation = Quaternion.AngleAxis(30, Vector3.back);
                p.rigid.velocity = p.rigid.rotation * velocity;
                p = makeProjectile();//left bullet
                p.transform.rotation = Quaternion.AngleAxis(-30, Vector3.back);
                p.rigid.velocity = p.rigid.rotation * velocity;
                break;
        }

    }

    //make the projectile
    public Projectile makeProjectile()
    {
        GameObject bullet = Instantiate(_def.projectilePrefab);
        if (transform.parent.gameObject.tag == "Hero")
        {
            bullet.tag = "ProjectileHero";
        }
        else
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
