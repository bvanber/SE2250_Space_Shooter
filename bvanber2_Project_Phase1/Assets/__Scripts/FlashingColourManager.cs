using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//an enum to list the different flashing status's
public enum Status
{
    on,
    fade, 
    off
}

public class FlashingColourManager : MonoBehaviour
{
    [Header ("Set in Inspector")]
    public Color [] colors;

    private int _start;
    private int _end;
    private float _switch = 1f;
    public Status status=Status.off;//default status
    //private Transform[] _allChildren=new Transform[5];
    public int _size;

    // Start is called before the first frame update
    void Start()
    {
        //initialize the starting two colours
        _start = 0;
        _end = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //call the proper color switching method based on the status
        switch (status)
        {
            case (Status.on):
                ColorSwitch();
                break;
            case (Status.fade):
                Fade();
                break;
                
        }
    }
    //regular colour switching
    public void ColorSwitch()
    {
        //timer for knowing when to switch colors
        if (_switch > 0)
        {
            _switch -= 0.1f;
        }
        else if (_switch <= 0)
        {
            _start = (_start + 1) % colors.Length;
            _end = (_end + 1) % colors.Length;
            _switch = 3f;//reset timer
           
            List<Renderer> _allChildren = GetChildren(gameObject);//get all eligible children to color
            foreach(Renderer child in _allChildren)
            {            
                //change the color of all the children gradually
                child.material.color = Color.Lerp(colors[_start], colors[_end], 0.1f);               
            }
                
        }
    }

    //colour switching as the effect is wearing off
    public void Fade()
    {
        //timer for knowing when to switch colors
        if (_switch > 0)
        {
            _switch -= 0.1f;
        }
        else if (_switch <= 0)
        {
            _switch = 3f;//reset timer
            Color end=colors[_start];
            Color start = Color.white;
            if (_start%2==0)//need this otherwise the colours wont alternate with white
            {
                start= colors[_start];
                end= Color.white;
            }
            _start = (_start+1) % colors.Length;
            
            List<Renderer> _allChildren = GetChildren(gameObject);//get all of the eligible children to color
            foreach (Renderer child in _allChildren)
            {    
                //change their colors from color to white or vice versa (instead of color to color for the fade)
                child.material.color = Color.Lerp(start, end, 0.1f);            
            }
        }        
    }

    //return the colors back to thier original at the end of the powerup
    public void ReturnToDefault()
    {
        List<Renderer> _allChildren = GetChildren(gameObject);//get all children needed to be changed
        foreach (Renderer child in _allChildren)
        {
            child.material.color = Color.white;            
        }
    }

    //the list of transfomrs needs to be gotten every time the color is changed
    //a single list cant be made once at the beginning or it wont work
    private List<Renderer> GetChildren(GameObject g)
    {
        List<Renderer> list = new List<Renderer>();
        Transform[] _allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in _allChildren)
        {
            Renderer rend = child.gameObject.GetComponent<Renderer>();
            //renderer shouldnt be null, and by preference we dont want to change the colors of the shield or collar
            if (rend!= null && child.gameObject.tag != "Shield" && child.gameObject.tag != "Collar")
            {
                list.Add(rend);
            }
        }
        return list;
    }
    
}
