using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zaznaczenie : MonoBehaviour
{
    public Zdarzenie.dProceduraObiektu Zaznacz, Odznacz;

    public Texture2D Neutral, Active, Selected;

    public Renderer rMesh;

    public Obiekt Parent;

    public Zaznaczacz _zaznaczacz;

    public bool _bJuzPoszedl, _bKlikniety, _bZaznaczony;

    public bool LockSelection;

	void Start ()
    {
        Parent = GetComponent<Obiekt>();

        if (Parent != null)
            Parent.Init_Delegates(null, ClickDown, ClickUp, null, null, null, Hover, Leave, null, CoursorContactWithThis, CoursorContactWithOther, CoursorNoContact);
        
        rMesh = gameObject.GetComponentInChildren<Renderer>();
        if (Neutral != null)
            rMesh.material.mainTexture = Neutral;

        _bJuzPoszedl = true; _bKlikniety = _bZaznaczony = false;

        _zaznaczacz = GameObject.Find("Baza").GetComponent<Zaznaczacz>();
        Init_Delegates();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) == true)
            if (!LockSelection)
                LockSelection = true;
        if (Input.GetKeyUp(KeyCode.LeftShift) == true)
            if (LockSelection)
                LockSelection = false;
    }

    private void Init_Delegates()
    {
        if (_zaznaczacz != null)
        {
            Zdarzenie.InitDelegate(ref Zaznacz, _zaznaczacz.Zaznacz);
            Zdarzenie.InitDelegate(ref Odznacz, _zaznaczacz.Odznacz);
        }
    }

    private void ClickDown(Collider sender, RaycastHit mouseArgs)
    {
        _bKlikniety = true;
    }
    private void ClickUp(Collider sender, RaycastHit mouseArgs)
    {
        _bKlikniety = false;
        _bZaznaczony = Obiekt.SwitchToggle(_bZaznaczony);
        if (_bZaznaczony)
        {
            if (Zaznacz != null)
                Zaznacz(gameObject);
        }
        else
        {
            if (Odznacz != null)
                Odznacz(gameObject);
        }
    }
    private void Hover(Collider sender, RaycastHit mouseArgs)
    {
        _bJuzPoszedl = false;
        rMesh.material.mainTexture = Active;
    }
    private void Leave(Collider sender, RaycastHit mouseArgs)
    {
        _bJuzPoszedl = true;

        if (_bZaznaczony)
            rMesh.material.mainTexture = Selected;
        else
            rMesh.material.mainTexture = Neutral;

        if(_bKlikniety)
        {
            Odznacz(gameObject);
            _bKlikniety = false;
            _bZaznaczony = false;
            rMesh.material.mainTexture = Neutral;
        }
    }
    private void CoursorNoContact(Collider sender, RaycastHit mouseArgs)
    {
        ClickOutsideThisCollider(sender);
    }
    private void CoursorContactWithOther(Collider sender, RaycastHit mouseArgs)
    {
        ClickOutsideThisCollider(sender);
    }
    private void CoursorContactWithThis(Collider sender, RaycastHit mouseArgs)
    {
        Parent.Komunikat = name + " : " + transform.position.ToString();
    }
    private void ClickOutsideThisCollider(Collider sender)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_bZaznaczony && !LockSelection && sender.gameObject.transform.parent!=GameObject.Find("CF_Edit").transform)
            {
                Odznacz(gameObject);
                _bZaznaczony = false;
                rMesh.material.mainTexture = Neutral;
            }
        }
    }
    public void FixedOdznacz()
    {
        if (_bZaznaczony && !LockSelection)
        {
            Odznacz(gameObject);
            _bZaznaczony = false;
            rMesh.material.mainTexture = Neutral;
        }
    }
}
