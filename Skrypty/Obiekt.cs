using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obiekt : MonoBehaviour
{
    Zdarzenie.dProceduraMyszki Hover, L_Click, L_ClickDown, L_ClickUp, P_Click, P_ClickDown, P_ClickUp, Leave, CoursorContact, CoursorContactWithThis, CoursorContactWithOther, CoursorNoContact;
    
    public bool _bJuzPoszedl, ZabronWyswietlaniaKomunikatow;

    public Ray _r;

    public RaycastHit _h;
    public RaycastHit GetMouseArgs()
    {
        return _h;
    }

    public string Komunikat;

    public Komunikat _komunikat;

    void Start ()
    {
        _komunikat = GameObject.Find("Komunikat_Kursor").GetComponent<Komunikat>();

        if (Komunikat == null || Komunikat == "")
            Komunikat = "Przykladowy Komunikat";

        _bJuzPoszedl = true;

        Init_Delegates
            (
            Obiekt_Klik_Lewy,
            Obiekt_KlikDown_Lewy,
            Obiekt_KlikUp_Lewy,

            Obiekt_Klik_Prawy,
            Obiekt_KlikDown_Prawy,
            Obiekt_KlikUp_Prawy,

            Obiekt_Hover,
            Obiekt_Leave,

            Global_CoursorContact,
            Global_CoursorContactWithThis,
            Global_CoursorContactWithOther,
            Global_CoursorNoContact
            );

    }
    public void Init_Delegates
        (
        Zdarzenie.dProceduraMyszki L_Click,
        Zdarzenie.dProceduraMyszki L_ClickDown,
        Zdarzenie.dProceduraMyszki L_ClickUp,

        Zdarzenie.dProceduraMyszki P_Click,
        Zdarzenie.dProceduraMyszki P_ClickDown,
        Zdarzenie.dProceduraMyszki P_ClickUp,

        Zdarzenie.dProceduraMyszki Hover,
        Zdarzenie.dProceduraMyszki Leave,

        Zdarzenie.dProceduraMyszki CoursorContact,
        Zdarzenie.dProceduraMyszki CoursorContactWithThis,
        Zdarzenie.dProceduraMyszki CoursorContactWithOther,
        Zdarzenie.dProceduraMyszki CoursorNoContact
        )
    {
        Zdarzenie.InitDelegate(ref this.Hover, Hover);

        Zdarzenie.InitDelegate(ref this.L_Click, L_Click);
        Zdarzenie.InitDelegate(ref this.L_ClickDown, L_ClickDown);
        Zdarzenie.InitDelegate(ref this.L_ClickUp, L_ClickUp);

        Zdarzenie.InitDelegate(ref this.P_Click, P_Click);
        Zdarzenie.InitDelegate(ref this.P_ClickDown, P_ClickDown);
        Zdarzenie.InitDelegate(ref this.P_ClickUp, P_ClickUp);

        Zdarzenie.InitDelegate(ref this.Leave, Leave);

        Zdarzenie.InitDelegate(ref this.CoursorContact, CoursorContact);
        Zdarzenie.InitDelegate(ref this.CoursorContactWithThis, CoursorContactWithThis);
        Zdarzenie.InitDelegate(ref this.CoursorContactWithOther, CoursorContactWithOther);

        if (CoursorNoContact != null)
            this.CoursorNoContact += CoursorNoContact;
    }
    

    public static bool SwitchToggle(bool SwitchTurnedOn)
    {
        if (SwitchTurnedOn)
            return false;
        else
            return true;
    }

    void Update ()
    {
        Update_Kursor();
	}

    public virtual void Update_Kursor()
    {
        _r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_r, out _h))
        {
            if(CoursorContact != null) CoursorContact(_h.collider,_h);

            if (_h.collider.name == this.name)
            {
                if (CoursorContactWithThis != null) CoursorContactWithThis(_h.collider, _h);

                if (_bJuzPoszedl)
                {
                    if (Hover != null) Hover(_h.collider, _h);
                    _bJuzPoszedl = false;
                    return;
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (L_ClickDown != null) L_ClickDown(_h.collider, _h); return;
                }
                if (Input.GetMouseButton(0))
                {
                    if (L_Click != null) L_Click(_h.collider,_h); return;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (L_ClickUp != null) L_ClickUp(_h.collider, _h); return;
                }
                
                if (Input.GetMouseButtonDown(1))
                {
                    if (P_ClickDown != null) P_ClickDown(_h.collider, _h); return;
                }
                if (Input.GetMouseButton(1))
                {
                    if (P_Click != null) P_Click(_h.collider, _h); return;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    if (P_ClickUp != null) P_ClickUp(_h.collider, _h); return;
                }
            }
            else
            {
                if (CoursorContactWithOther != null) CoursorContactWithOther(_h.collider, _h);

                if (!_bJuzPoszedl)
                {
                    if (Leave != null) Leave(_h.collider, _h);

                    _bJuzPoszedl = true;

                    return;
                }
            }
        }
        else
        {
            if (CoursorNoContact != null) CoursorNoContact(GetComponent<Collider>(), _h);

            if (!_bJuzPoszedl)
            {
                if (Leave != null) Leave(GetComponent<Collider>(), _h);

                _bJuzPoszedl = true;

                return;
            }
        }
    }



    public virtual void Obiekt_Klik_Lewy(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Lewy Click");
    }
    public virtual void Obiekt_KlikDown_Lewy(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Lewy Click Down");
    }
    public void Obiekt_KlikUp_Lewy(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Lewy Click Up");
    }



    public virtual void Obiekt_Klik_Prawy(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Prawy Click");
    }
    public virtual void Obiekt_KlikDown_Prawy(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Prawy Click Down");
    }
    public void Obiekt_KlikUp_Prawy(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Prawy Click Up");
    }



    public void Obiekt_Hover(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Hover");

        if (!ZabronWyswietlaniaKomunikatow)
            if (_komunikat != null)
                _komunikat.WyswietlKomunikat(Komunikat);
    }
    public void Obiekt_Leave(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : Leave");
    }
    


    public void Global_CoursorContact(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : CoursorCollision");
    }
    public void Global_CoursorContactWithThis(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : CoursorCollisionWithThis");
    }
    public void Global_CoursorContactWithOther(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : CoursorCollisionWithOther(" + sender.gameObject.name + ")");
    }
    public void Global_CoursorNoContact(Collider sender, RaycastHit mouseArgs)
    {
        Debug.Log(this.name + " : CoursorNoCollision");
    }



    public static List<Vector3> Get_Spread_V3(Vector3 OriginalPosition, Vector3 SpreadLenght, int ItemCount)//, Vector3 Cos, Vector3 Sin)
    {
        List<Vector3> Zwracana = new List<Vector3>();

        if (ItemCount == 1)
        {
            Zwracana.Add(
                new Vector3(0f, 0.01f, 0f));
        }
        else
        {
            Vector3 Temp = new Vector3(
                SpreadLenght.x / 2f,
                SpreadLenght.y / 2f,
                SpreadLenght.z / 2f
                );

            float fTemp;

            for (float i = 0; i < ItemCount; i++)
            {
                fTemp = i / (ItemCount - 1);
                Zwracana.Add(
                    new Vector3(
                        SpreadLenght.x * fTemp - Temp.x,
                        SpreadLenght.y * fTemp - Temp.y,
                        SpreadLenght.z * fTemp - Temp.z
                        )
                    );
            }
        }
        
        return Zwracana;
    }

    public static Vector3 GetStepOf_MoveIteration(Vector3 Path, float Sensitivity)
    {
        Vector3 Zwracana = new Vector3(Path.x / Sensitivity, Path.y / Sensitivity, Path.z / Sensitivity);

        return Zwracana;
    }

    public static void SwitchToggle(ref bool OriginalBoolObject)
    {
        if (OriginalBoolObject)
            OriginalBoolObject = false;
        else
            OriginalBoolObject = true;
    }

    public static bool SwitchToggle(bool OriginalBoolObject, bool NewValue)
    {
        if (OriginalBoolObject)
            return false;
        else
            return true;
    }

    public static bool IsAtPosition(Vector3 ObjectPosition, Vector3 TargetPosition, float TolerableOffset)
    {
        if (ObjectPosition.x <= TargetPosition.x + TolerableOffset && ObjectPosition.x >= TargetPosition.x - TolerableOffset)
            if (ObjectPosition.y <= TargetPosition.y + TolerableOffset && ObjectPosition.y >= TargetPosition.y - TolerableOffset)
                if (ObjectPosition.z <= TargetPosition.z + TolerableOffset && ObjectPosition.z >= TargetPosition.z - TolerableOffset)
                    return true;

        return false;
    }
    private static bool AnyChildEquals(Transform CheckedObject, Transform ComparedObject)
    {
        if (CheckedObject.name==ComparedObject.name)
            return true;

        bool DzieciMajaTenObiekt = false;

        for (int i = 0; i < CheckedObject.childCount; i++)
            if (CheckedObject.GetChild(i).name == ComparedObject.name)
                DzieciMajaTenObiekt = true;
        
        return DzieciMajaTenObiekt;
    }
    public static string GenerateGlobalName(string prefabName)
    {
        string newName = "";

        if(prefabName != null && prefabName != "")
        {
            newName = prefabName;
        }
        else
        {
            newName = "_obij_";
        }

        GameObject Found;

        for (int i = 0; i<100; i++)
        {
            Found = GameObject.Find(newName + i.ToString());

            if (Found == null)
            {
                newName += i.ToString();
                break;
            }
        }

        return newName;
    }

    public static List<int> IdsOf(List<Zaznaczenie> Lista)
    {
        List<int> Ids = new List<int>();

        foreach(Zaznaczenie z in Lista)
        {
            Ids.Add(GetIdFrom(z.gameObject.name));
        }

        return Ids;
    }

    public static int GetIdFrom(string name)
    {
        string sId = "";

        foreach (char c in name)
            if (char.IsDigit(c))
                sId += c;

        return int.Parse(sId);
    }

    public static Vector3 GenerateOriginalPosition(Vector3 OriginalPosition, List<Vector3> Excluded)
    {
        foreach (Vector3 V in Excluded)
            if (OriginalPosition == V)
                return GenerateOriginalPosition(new Vector3(
                    OriginalPosition.x + 1f,
                    OriginalPosition.y,
                    OriginalPosition.z
                    ), Excluded);

        return OriginalPosition;
    }
}
