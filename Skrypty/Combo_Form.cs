using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_Form : MonoBehaviour
{
    public bool _bIdzDoPkt, _bIdzNaDol, _autoHide;

    public Vector3 StartPosition, EndPosition;

    public float Sensitivity, TolerableOffset;

    public Vector3 Step;

    float AutoHideTimer;
    public float AutoHideSensitivity = 1000;

    Obiekt _obiekt;

    public List<Obiekt> Lista;

    public List<Texture2D> Textury;

	void Start ()
    {
        GetComponent<Renderer>().material.mainTexture = Textury[1];

        _obiekt = GetComponent<Obiekt>();

        _obiekt.Init_Delegates(null, null, null, null, null, Right_ClickUp, null, null, null, null, null, null);
        
        transform.localPosition = StartPosition;

        MoveTill_Start(transform);

        if(_autoHide)
        AutoHideTimer = 0;
    }
	
	void Update ()
    {
        if(_bIdzDoPkt)
        {
            ProceedToMove();
        }
	}

    private void Left_ClickDown(Collider sender, RaycastHit mouseArgs)
    {
    }
    private void Left_ClickUp(Collider sender, RaycastHit mouseArgs)
    {
    }
    private void Right_ClickUp(Collider sender, RaycastHit mouseArgs)
    {
    }


    public void Init_Movement()
    {
        _bIdzDoPkt = true;

        if (_bIdzNaDol)
        {
            Step = new Vector3(
                (EndPosition.x - StartPosition.x) / Sensitivity,
                (EndPosition.z - StartPosition.z) / Sensitivity,
                (EndPosition.y - StartPosition.y) / Sensitivity);
        }
        else
        {
            Step = new Vector3(
                (StartPosition.x - EndPosition.x) / Sensitivity,
                (StartPosition.z - EndPosition.z) / Sensitivity,
                (StartPosition.y - EndPosition.y) / Sensitivity);
        }
    }
    public void ProceedToMove()
    {
        if (_bIdzDoPkt)
            if (_bIdzNaDol)
            {
                if (!Obiekt.IsAtPosition(transform.localPosition, EndPosition, TolerableOffset))
                    transform.Translate(Step, Space.Self);
                else
                {
                    transform.localPosition = EndPosition;

                    _bIdzDoPkt = false;
                }
            }
            else
            {
                if (!Obiekt.IsAtPosition(transform.localPosition, StartPosition, TolerableOffset))
                    transform.Translate(Step, Space.Self);
                else
                {
                    transform.localPosition = StartPosition;

                    _bIdzDoPkt = false;
                }
            }
    }

    public void MoveTill_End()
    {
        if (!_bIdzNaDol)
        {
            _bIdzNaDol = true;

            Init_Movement();
        }
    }
    public void MoveTill_Start(Transform sender)
    {
        if (sender.parent != transform)
            if (_bIdzNaDol)
            {
                _bIdzNaDol = false;

                Init_Movement();
            }
    }


    public Transform prefabButton, prefabVert, prefabText, prefabEdge;
    public void Add_ButtonToForm(GameObject Added)
    {
        Obiekt _tempObject = Added.AddComponent<Obiekt>();

        Lista.Add(_tempObject);

        Added.transform.parent = transform;

        Added.transform.rotation = transform.rotation;

        Added.transform.localPosition = new Vector3(0, 0.1f, 0);

        Added.name = Obiekt.GenerateGlobalName("_obiekt_");

        if (Textury.Count > 0)
        {
            Added.GetComponent<Renderer>().material.mainTexture = Textury[0];
        }

        Refresh_Btn(new Vector3(0, 0, 0));
    }
    public void Add_ButtonToForm(Transform prefab, string newName)
    {
        GameObject Added = Instantiate<GameObject>(prefabButton.gameObject, transform);

        Obiekt _obiekt = Added.GetComponent<Obiekt>();

        if (_obiekt != null)
            Lista.Add(_obiekt);

        Added.transform.parent = transform;

        Added.transform.rotation = transform.rotation;

        Added.transform.localPosition = new Vector3(0, 0.01f, 0);

        Added.name = Obiekt.GenerateGlobalName(newName);

        if (Textury.Count > 0)
        {
            Added.GetComponent<Renderer>().material.mainTexture = Textury[0];
        }

        Refresh_Btn(new Vector3(0, 0, 0));
    }
    public void Add_ButtonToForm(Transform prefab, string newName, Vector3 WantedOffset)
    {
        GameObject Added = Instantiate<GameObject>(prefabButton.gameObject, transform);

        Obiekt _obiekt = Added.GetComponent<Obiekt>();

        if (_obiekt != null)
            Lista.Add(_obiekt);

        Added.transform.parent = transform;

        Added.transform.rotation = transform.rotation;

        Added.transform.localPosition = new Vector3(0, 0.01f, 0);

        Added.name = Obiekt.GenerateGlobalName(newName);

        if (Textury.Count > 0)
        {
            Added.GetComponent<Renderer>().material.mainTexture = Textury[0];
        }

        Refresh_Btn(WantedOffset);
    }
    public GameObject Add_ButtonToForm(Transform prefab, string newName, Vector3 WantedOffset, bool GenerujGlobalneImie)
    {
        GameObject Added = Instantiate<GameObject>(prefabButton.gameObject, transform);

        Obiekt _obiekt = Added.GetComponent<Obiekt>();
        if (_obiekt != null)
            Lista.Add(_obiekt);


        Added.transform.parent = transform;

        Added.transform.rotation = transform.rotation;

        Added.transform.localPosition = new Vector3(0, 0.01f, 0);


        if (GenerujGlobalneImie)
            Added.name = Obiekt.GenerateGlobalName(newName);
        else
            Added.name = newName;

        _obiekt.Komunikat = Added.name;

        if (Textury.Count > 0)
        {
            Added.GetComponent<Renderer>().material.mainTexture = Textury[0];
        }

        Refresh_Btn(WantedOffset);

        return Added;
    }

    public void Refresh_Btn(Vector3 WantedOffset)
    {
        Refresh_BtnLocation(WantedOffset);

        Refresh_BtnRotation();
    }
    public void Refresh_BtnLocation(Vector3 WantedOffset)
    {
        List<Vector3> NewPositions = Obiekt.Get_Spread_V3(
            transform.position,
            new Vector3(0f, 0f, 4.6f),
            Lista.Count
            );


        for (int i = 0; i < Lista.Count; i++)
        {
            if (Lista[i] != null)
            {
                Lista[i].gameObject.transform.position = new Vector3(
                    transform.position.x + WantedOffset.x, 
                    transform.position.y + WantedOffset.y, 
                    transform.position.z + WantedOffset.z);
                Lista[i].gameObject.transform.Translate(NewPositions[i], Space.Self);
            }
        }
    }
    public void Refresh_BtnRotation()
    {
        foreach (Obiekt O in Lista)
            O.transform.rotation = transform.rotation;
    }
}
