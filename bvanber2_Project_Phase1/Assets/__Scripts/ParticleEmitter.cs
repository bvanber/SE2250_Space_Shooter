using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    public GameObject particle;
    private List<GameObject> _particles = new List<GameObject>();
    private List<GameObject> _toDelete=new List<GameObject>();
    public float sclaingFactor=0.5f;
    
    // Update is called once per frame
    void Update()
    {
        GameObject obj = Instantiate(particle);
        obj.transform.position = gameObject.transform.position;
        Vector3 dir = transform.position - transform.root.position;
        obj.GetComponent<Rigidbody>().velocity = dir;
        _particles.Add(obj);

        foreach(GameObject p in _particles)
        {
            p.transform.localScale *= sclaingFactor;
            if (p.transform.localScale.x <= 0.01f) _toDelete.Add(p);
        }
        foreach(GameObject d in _toDelete)
        {
            _particles.Remove(d);
            Destroy(d);
        }
        _toDelete.Clear();
    }
}
