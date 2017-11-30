using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    Obiekt _obiekt;

    public Obiekt V1, V2;
    
    void Start ()
    {
        _obiekt = GetComponent<Obiekt>();

        Update_transform();
    }

    void Update ()
    {
        //Update_transform();
	}

    public void Update_transform()
    {
        if (V1 != null && V2 != null)
        {
            Vector3 Temp = new Vector3(
                V2.transform.position.x - V1.transform.position.x,
                V2.transform.position.y - V1.transform.position.y,
                V2.transform.position.z - V1.transform.position.z
                );

            Update_location(Temp);

            Update_scale(Temp);

            Update_Rotation();
        }
    }

    public void Update_location(Vector3 Difference)
    {
        transform.position = new Vector3(
            V1.transform.position.x + Difference.x / 2f,
            V1.transform.position.y + Difference.y / 2f,
            V1.transform.position.z + Difference.z / 2f);
    }
    public void Update_scale(Vector3 Difference)
    {
        float NewScale = Difference.x * Difference.x + Difference.z * Difference.z;

        transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y, Mathf.Sqrt(NewScale));

    }
    public void Update_Rotation()
    {
        transform.LookAt(V2.transform.position);
    }

    public float GetLenght()
    {
        Vector3 Lenght = V2.transform.position - V1.transform.position;

        return Lenght.magnitude;
    }

    public Obiekt GetSecondVert(Obiekt FirstVert)
    {
        if (FirstVert == V1)
            return V2;

        if (FirstVert == V2)
            return V1;

        return null;
    }
}
