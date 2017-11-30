using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CF_Main : MonoBehaviour
{
    Obiekt _obiekt;

    Combo_Form _form_cb;

    Obiekt _btnAddVert, _btnDijkstra;

    Zaznaczacz _zaznaczacz;

    Baza _baza;

    GameObject ParentVerts;

    void Start()
    {
        Init_Obiekt();

        Init_Baza();

        Init_Form();

        Init_Dijkstra();
    }
	
    void Update ()
    {

    }



    private void Init_Obiekt()
    {
        _obiekt = GetComponent<Obiekt>();

        _obiekt.Init_Delegates(null, null, null, null, null, null, Hover, Leave, null, null, null, null);
    }



    private void Init_Baza()
    {
        GameObject Baza = GameObject.Find("Baza");

        _zaznaczacz = Baza.GetComponent<Zaznaczacz>();

        _baza = _zaznaczacz.gameObject.GetComponent<Baza>();

        ParentVerts = _baza.gameObject.transform.GetChild(2).gameObject;
    }



    private void Init_Form()
    {
        _form_cb = GetComponent<Combo_Form>();

        if (_form_cb != null)
        {
            _form_cb.Add_ButtonToForm(_form_cb.prefabButton, "_btnAddVert_", new Vector3(2.5f, 0.1f, 0));
            //_form_cb.Add_ButtonToForm(_form_cb.prefabButton, "_btnDijkstra", new Vector3(2.5f, 0.1f, 0));

            if (_form_cb.Lista.Count >= 1)
                _btnAddVert = _form_cb.Lista[0];

            if (_form_cb.Lista.Count >= 2)
                _btnDijkstra = _form_cb.Lista[1];
        }

        if (_btnAddVert != null)
        {
            _btnAddVert.Init_Delegates(null, null, _btnAddVert_ClickUp, null, null, null, null, null, null, null, null, null);

            GameObject Text = Instantiate<GameObject>(_form_cb.prefabText.gameObject, _btnAddVert.transform);
            if (Text != null)
            {
                Text T = Text.GetComponent<Text>();

                if (T != null)
                    T.text = "+";
            }
        }

        if (_btnDijkstra != null)
        {
            _btnDijkstra.Init_Delegates(null, null, _btnDijkstra_ClickUp, null, null, null, null, null, null, null, null, null);

            GameObject Text = Instantiate<GameObject>(_form_cb.prefabText.gameObject, _btnDijkstra.transform);
            if (Text != null)
            {
                Text T = Text.GetComponent<Text>();

                if (T != null)
                    T.text = "D";
            }
        }
    }
    private void Hover(Collider sender, RaycastHit mouseArgs)
    {
        _form_cb.MoveTill_End();
    }
    private void Leave(Collider sender, RaycastHit mouseArgs)
    {
        _form_cb.MoveTill_Start(sender.transform);
    }

    private void _btnAddVert_ClickUp(Collider sender, RaycastHit mouseArgs)
    {
        GameObject _object = Instantiate<GameObject>(_form_cb.prefabVert.gameObject, ParentVerts.transform);

        _object.transform.position = GetOriginalGlobalPosition();

        _object.name = Obiekt.GenerateGlobalName("O_");

        _baza.Verts.Add(_object.GetComponent<Obiekt>());

        Debug.Log("ClickUp");
    }
    public static Vector3 GetOriginalGlobalPosition()
    {
        List<Vector3> Excluded = new List<Vector3>();

        int i = 0;

        string GO_name = "O_";

        GameObject GO = GameObject.Find(GO_name + i.ToString());

        while (GO != null)
        {
            Excluded.Add(GO.transform.position);

            ++i;

            GO = GameObject.Find(GO_name + i.ToString());
        }

        return Obiekt.GenerateOriginalPosition(new Vector3(0, 0, 0), Excluded);
    }




    Dijkstra _dijkstra;
    private void Init_Dijkstra()
    {
        _dijkstra = _baza.gameObject.GetComponent<Dijkstra>();
    }
    private void _btnDijkstra_ClickUp(Collider sender, RaycastHit mouseArgs)
    {
        _dijkstra.Init_Algorithm(_baza.Verts, _zaznaczacz.Schowek[0].gameObject.GetComponent<Obiekt>(), _zaznaczacz.Schowek[1].gameObject.GetComponent<Obiekt>(), _baza.Edges);
    }
}
