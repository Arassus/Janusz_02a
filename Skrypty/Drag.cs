using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Obiekt _obiekt;
    Zaznaczenie _zaznaczenie;
    Zaznaczacz _zaznaczacz;
    
    float X, Y, Z;

    bool bIsDragging;
    public void SetDrag(bool NewValue)
    {
        bIsDragging = NewValue;
    }

    RaycastHit mouseArgs;

    Baza _baza;

	void Start ()
    {
        _baza = GameObject.Find("Baza").GetComponent<Baza>();

        bIsDragging = false;

        _obiekt = GetComponent<Obiekt>();
        
        if(_obiekt!=null)
        {
            _obiekt.Init_Delegates(null, null, null, null, Prawy_ClickDown, Prawy_ClickUp, null, null, null, null, CheckPrawyClickUp_EveryOtherObj, CheckPrawyClickUp_EveryOtherObj);
        }

        _zaznaczenie = GetComponent<Zaznaczenie>();

        _zaznaczacz = _baza.gameObject.GetComponent<Zaznaczacz>();//GameObject.Find("Baza").GetComponent<Zaznaczacz>();

        Init_Komunikat();
	}

    void Update()
    {
        if (bIsDragging)
        {
            mouseArgs = _zaznaczenie.Parent.GetMouseArgs();

            if(_zaznaczacz!=null)
            {
                ProceedToDrag(GetComponent<Collider>(), mouseArgs);

                SetKomunikat(name + " : " + transform.position.ToString());
            }
        }
    }


    public void ProceedToDrag(Collider sender, RaycastHit mouseArgs)
    {
        if (!_zaznaczacz.bOffsetsInitialized)
        {
            _zaznaczacz.OffsetsToThis.Clear();

            foreach (Zaznaczenie _z in _zaznaczacz.Schowek)
            {
                _zaznaczacz.OffsetsToThis.Add(

                                                new Vector3(
                                                            _z.transform.position.x - sender.transform.position.x,

                                                            transform.position.y,

                                                            _z.transform.position.z - sender.transform.position.z
                                                            )
                                
                                            );
            }
            _zaznaczacz.Set_bOffsetsInitialized(true);
        }
        for (int i = 0; i < _zaznaczacz.Schowek.Count; i++)
        {
            X = mouseArgs.point.x; X -= mouseArgs.point.x % _baza.Get_GridX();

            Z = mouseArgs.point.z; Z -= mouseArgs.point.z % _baza.Get_GridZ();

            _zaznaczacz.Schowek[i].transform.position = new Vector3(
                                                                    mouseArgs.point.x - mouseArgs.point.x % _baza.Get_GridX() + _zaznaczacz.OffsetsToThis[i].x, 

                                                                    transform.position.y + _zaznaczacz.OffsetsToThis[i].y,

                                                                    mouseArgs.point.z - mouseArgs.point.z % _baza.Get_GridZ() + _zaznaczacz.OffsetsToThis[i].z
                                                                    );
        }
    }

    private void Prawy_ClickDown(Collider sender, RaycastHit mouseArgs)
    {
        if (_zaznaczenie != null)
            if (_zaznaczenie._bZaznaczony)
            {
                this.mouseArgs = mouseArgs;
                bIsDragging = true;
            }
    }

    private void Prawy_ClickUp(Collider sender, RaycastHit mouseArgs)
    {
        MouseDragToggle();
    }
    private void CheckPrawyClickUp_EveryOtherObj(Collider sender, RaycastHit mouseArgs)
    {
        if(Input.GetMouseButtonUp(1))
            MouseDragToggle();
    }
    private void MouseDragToggle()
    {
        if (bIsDragging)
            bIsDragging = false;

        if (_zaznaczacz.Get_OffsetIsInitialized())
            _zaznaczacz.bOffsetsInitialized = false;
    }
    

    Komunikat _komunikat;

    public Zdarzenie.dProceduraObiektu Update_Komunikat;

    private void Init_Komunikat()
    {
        _komunikat = GameObject.Find("Komunikat_Kursor").GetComponent<Komunikat>();
    }
    public void SetKomunikat(string newKomunikat)
    {
        if (_komunikat != null)
            _komunikat.WyswietlKomunikat(newKomunikat);

        if (_baza != null)
            _baza.Update_Edges();
    }
}
