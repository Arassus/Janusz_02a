using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zaznaczacz : MonoBehaviour
{
    public List<Zaznaczenie> Schowek;

    public List<Vector3> OffsetsToThis;

    public bool bOffsetsInitialized;
    public void Set_bOffsetsInitialized(bool NewValue)
    {
        bOffsetsInitialized = NewValue;
    }

    public CF_Edit _cf_edit;

    public void Zaznacz(GameObject Obiekt)
    {
        Zaznaczenie _zaznaczenie = Obiekt.GetComponent<Zaznaczenie>();

        if (_zaznaczenie != null)
            Schowek.Add(_zaznaczenie);

        Set_OffsetIsInitialized(false);

        _cf_edit.StartMoving();

        if (SelectionChanged != null)
            SelectionChanged(gameObject);
    }

    public void Odznacz(GameObject Obiekt)
    {
        Zaznaczenie _zaznaczenie = Obiekt.GetComponent<Zaznaczenie>();

        if (_zaznaczenie != null)
            Schowek.Remove(_zaznaczenie);

        Set_OffsetIsInitialized(false);

        if(Schowek.Count<0)
        _cf_edit.StopMoving();

        if (SelectionChanged != null)
            SelectionChanged(gameObject);
    }

    public void StopDragging()
    {
        foreach(Zaznaczenie Z in Schowek)
        {
            Z.GetComponent<Drag>().SetDrag(false);
            Z.Odznacz(Z.gameObject);

        }
    }
    public void WyczyscSchowek()
    {
        Schowek.Clear();
    }
    public void UsunZaznaczone()
    {
        foreach (Zaznaczenie Z in Schowek)
            Destroy(Z.gameObject);

        WyczyscSchowek();
    }

    public void Set_OffsetIsInitialized(bool NewValue)
    {
        bOffsetsInitialized = NewValue;
    }
    public bool Get_OffsetIsInitialized()
    {
        return bOffsetsInitialized;
    }

    void Start ()
    {
        bOffsetsInitialized = false;
    }
	
	void Update ()
    {

	}

    public Zdarzenie.dProceduraObiektu SelectionChanged;



}
