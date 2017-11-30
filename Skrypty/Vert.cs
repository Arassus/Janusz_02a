using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vert : MonoBehaviour
{
    Obiekt _obiekt;
    
    public float Z
    {
        get
        {
            return transform.position.y;
        }
    }
    public float X
    {
        get
        {
            return transform.position.x; 
        }
        set
        {
            transform.position = new Vector3(value, Z, Y);
        }
    }
    public float Y
    {
        get
        {
            return transform.position.z;
        }
        set
        {
            transform.position = new Vector3(X, Z, value);
        }
    }


    void Start ()
    {
        _obiekt = GetComponent<Obiekt>();


	}
    
    void Update ()
    {
		
	}
}
