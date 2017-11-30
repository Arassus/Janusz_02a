using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baza : MonoBehaviour
{

    public List<Edge> Edges;
    public void Update_Edges()
    {
        foreach (Edge e in Edges)
        {
            if (e.V1 == null || e.V2 == null)
            {
                Destroy(e.gameObject);

                Edges.Remove(e);
            }
            e.Update_transform();
        }
    }
    public List<Edge> GetEdgesBy(Obiekt Vert)
    {
        List<Edge> Returned = new List<Edge>();

        foreach(Edge e in Edges)
        {
            if(e.V1==Vert ||e.V2==Vert)
            {
                Returned.Add(e);
            }
        }

        return Returned; 
    }
    public Edge GetEdge(Obiekt V1, Obiekt V2)
    {
        foreach(Edge e in Edges)
        {
            if ((e.V1 == V1 || e.V2 == V1) && (e.V1 == V2 || e.V2 == V2))
                return e;
        }

        return null;
    }

    public List<Obiekt> Verts;
    public void Update_Verts()
    {
        foreach (Obiekt o in Verts)
        {
            if (o == null)
                Verts.Remove(o);
        }
    }

    public float GridX = 1, GridZ = 1;

    public float Get_GridX()
    {
        return GridX;
    }
    public void Set_GridX(float NewValue)
    {
        GridX = NewValue;
    }

    public float Get_GridZ()
    {
        return GridZ;
    }
    public void Set_GridZ(float NewValue)
    {
        GridZ = NewValue;
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
