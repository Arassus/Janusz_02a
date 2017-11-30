using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CF_Edit : MonoBehaviour
{
    Obiekt _obiekt;

    Combo_Form _form;

    public Zaznaczacz _zaznaczacz;
    Baza _baza;
    GameObject ParentEdges;

    public List<Obiekt> Buttons;

    void Start()
    {
        _obiekt = GetComponent<Obiekt>();

        _form = GetComponent<Combo_Form>();

        _zaznaczacz = GameObject.Find("Baza").GetComponent<Zaznaczacz>();

        _baza = _zaznaczacz.gameObject.GetComponent<Baza>();

        ParentEdges = _baza.gameObject.transform.GetChild(1).gameObject;

        if (_zaznaczacz != null)
            Zdarzenie.InitDelegate(ref _zaznaczacz.SelectionChanged, SelectionChanged);

        Init_Dijkstra();
    }

    void Update()
    {

    }

    public void StartMoving()
    {
        if (_form != null)
        {
            _form.MoveTill_End();
        }
    }
    public void StopMoving()
    {
        if (_form != null)
        {
            _form.MoveTill_Start(transform);
        }
    }

    public void SelectionChanged(GameObject sender)
    {
        Update_Controls();
    }


    public void Update_Controls()
    {
        Clear_Buttons();

        Init_Buttons();
    }

    public void Clear_Buttons()
    {
        foreach (Obiekt o in _form.Lista)
            Destroy(o.gameObject);

        _form.Lista.Clear();

        if (Buttons != null)
            Buttons.Clear();
    }
    public void Init_Buttons()
    {
        List<int> ID = Obiekt.IdsOf(_zaznaczacz.Schowek);

        if (_zaznaczacz.Schowek.Count==0)
        {
            StopMoving();
            return;
        }
        for (int i = 0; i < _zaznaczacz.Schowek.Count; i++)
        {
            Debug.Log(i.ToString() + " : " + _zaznaczacz.Schowek[i].name + " : O_" + i.ToString());

            Create_RemoveButton("B_"+ID[i].ToString(), new Vector3(-3f, 0.1f, 0f));
            
        }

        Create_EdgeButton("E_0", new Vector3(-3f, 0.1f, 0f));
        Create_DijkstraButton("D_0", new Vector3(-3f, 0.1f, 0f));

    }
    public void Create_RemoveButton(string NewName, Vector3 WantedOffset)
    {
        GameObject NowyGuzik;

        NowyGuzik = _form.Add_ButtonToForm(_form.prefabButton, NewName, WantedOffset, false);

        GameObject Napis;

        Napis = Instantiate(_form.prefabText.gameObject, NowyGuzik.transform);

        Text T = Napis.GetComponent<Text>();

        T.text = "X";

        T.color = Color.red;

        Obiekt NG_obiekt = NowyGuzik.GetComponent<Obiekt>();

        if (NG_obiekt != null)
        {
            Buttons.Add(NG_obiekt);

            NG_obiekt.Init_Delegates(null, null, RemoveButton_Click, null, null, null, null, null, null, null, null, null);
        }
    }
    public void Create_DijkstraButton(string NewName, Vector3 WantedOffset)
    {
        GameObject NowyGuzik;

        NowyGuzik = _form.Add_ButtonToForm(_form.prefabButton, NewName, WantedOffset, false);

        GameObject Napis;

        Napis = Instantiate(_form.prefabText.gameObject, NowyGuzik.transform);

        Text T = Napis.GetComponent<Text>();

        T.text = NewName;

        T.color = Color.green;

        Obiekt NG_obiekt = NowyGuzik.GetComponent<Obiekt>();

        if (NG_obiekt != null)
        {
            Buttons.Add(NG_obiekt);

            NG_obiekt.Init_Delegates(null, null, _btnDijkstra_ClickUp, null, null, null, null, null, null, null, null, null);
        }
    }
    public void Create_EdgeButton(string NewName, Vector3 WantedOffset)
    {
        GameObject NowyGuzik;

        NowyGuzik = _form.Add_ButtonToForm(_form.prefabButton, NewName, WantedOffset, false);

        GameObject Napis;

        Napis = Instantiate(_form.prefabText.gameObject, NowyGuzik.transform);

        Text T = Napis.GetComponent<Text>();

        T.text = "Edge";

        T.color = Color.green;

        Obiekt NG_obiekt = NowyGuzik.GetComponent<Obiekt>();

        if (NG_obiekt != null)
        {
            Buttons.Add(NG_obiekt);

            NG_obiekt.Init_Delegates(null, null, EdgeButton_Click, null, null, null, null, null, null, null, null, null);
        }
    }


    public void RemoveButton_Click(Collider sender, RaycastHit mouseArgs)
    {
        int ID = Obiekt.GetIdFrom(sender.gameObject.name);

        GameObject Wierzcholek = GameObject.Find("O_" + ID.ToString());

        _zaznaczacz.Schowek.Remove(Wierzcholek.GetComponent<Zaznaczenie>());
        
        Obiekt _obiekt_Wierzcholek = Wierzcholek.GetComponent<Obiekt>();

        List<Edge> _edges_DoUsusniecia = _baza.GetEdgesBy(_obiekt_Wierzcholek);

        foreach(Edge e in _edges_DoUsusniecia)
        {
            _baza.Edges.Remove(e);
            Destroy(e.gameObject);
        }
        _baza.Verts.Remove(_obiekt_Wierzcholek);
        
        Destroy(Wierzcholek);

        Update_Controls();

        _baza.Update_Edges();
    }
    public void EdgeButton_Click(Collider sender, RaycastHit mouseArgs)
    {
        GameObject _object;

        if (_form.prefabEdge != null)
            for (int i = 1; i < _zaznaczacz.Schowek.Count; i++)
            {
                _object = Instantiate<GameObject>(_form.prefabEdge.gameObject, ParentEdges.transform);

                _object.name = Obiekt.GenerateGlobalName("E_");

                Edge E = _object.GetComponent<Edge>();

                E.V1 = _zaznaczacz.Schowek[i - 1].GetComponent<Obiekt>();

                E.V2 = _zaznaczacz.Schowek[i].GetComponent<Obiekt>();

                _baza.Edges.Add(E);
            }
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
