using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    Zaznaczacz _zaznaczacz;
    Baza _baza;

    void Start()
    {
        _zaznaczacz = GetComponent<Zaznaczacz>();

        _baza = GetComponent<Baza>();
    }

    void Update()
    {

    }

    //List<Obiekt> V_Orig;

    public List<Obiekt> V;

    public List<Obiekt> G;

    public List<Obiekt> K;

    public float[] W;

    public Obiekt[] P;

    public List<Edge> E;

    public void Clear_All()
    {
        V.Clear();

        G.Clear();

        K.Clear();

        W = null;

        P = null;
    }

    public void Init_Algorithm(List<Obiekt> Verts, Obiekt Start, Obiekt End, List<Edge> Edges)
    {
        Obiekt TempVert;

        Debug.Log("WIIERZCHOLEK  :  " + Verts.Count.ToString());
        //V_Orig = Verts;

        V = Verts;

        E = Edges;

        Init_W(V.IndexOf(Start), V.Count);

        Init_P(V.Count);

        for (int i = 0, ID; i < Verts.Count; i++)
        {
            Debug.Log("WIIERZCHOLEK  :  " + i.ToString());

            ID = GetId_LowestVal(W, G);

            TempVert = V[ID];

            K = GetConnectedObjectsOf(TempVert);

            AddjustWeights(TempVert);
        }

        int Start_ID = V.IndexOf(Start), End_ID = V.IndexOf(End), step = End_ID;

        for (int i = 0; i < V.Count; i++)
        {
            if (i == Start_ID)
                break;

            P[step].gameObject.GetComponent<Zaznaczenie>().Zaznacz(P[step].gameObject);
            Debug.Log("WIIERZCHOLEK  :  " + P[step].name);

            step = V.IndexOf(P[step]);
        }
    }

    private void Init_W(int StartID, int count)
    {
        W = new float[count];

        for (int i = 0; i < count; i++)
        {
            if (i == StartID)
                W[i] = 0;

            W[i] = int.MaxValue;
        }
    }
    private void Init_P(int count)
    {
        P = new Obiekt[count];
    }

    private int GetId_LowestVal(float[] Tab, List<int> NotTheseIds)
    {
        bool CanDo = true;

        int Returned = 0;

        for (int i = 0; i < Tab.Length; i++)
        {
            CanDo = true;

            if (Tab[i] < Tab[Returned])
            {
                foreach (int NotThisOne in NotTheseIds)
                {
                    if (i == NotThisOne)
                        CanDo = false;
                }

                if (CanDo)
                    Returned = i;
            }
        }

        return Returned;
    }
    private int GetId_LowestVal(float[] Tab, List<Obiekt> NotTheseIds)
    {
        bool CanDo = true;

        int Returned = 0;

        for (int i = 0; i < Tab.Length; i++)
        {
            CanDo = true;

            if (Tab[i] < Tab[Returned])
            {
                foreach (Obiekt o in NotTheseIds)
                {
                    if (i == V.IndexOf(o))
                        CanDo = false;
                }

                if (CanDo)
                    Returned = i;
            }
        }

        return Returned;
    }

    private List<Obiekt> GetConnectedObjectsOf(Obiekt _obiekt)
    {
        List<Obiekt> Returned = new List<Obiekt>();

        Obiekt temp;

        foreach (Edge e in E)
        {/*
            if (e.V1 == _obiekt)
                Returned.Add(e.V2);
            if (e.V2 == _obiekt)
                Returned.Add(e.V1);
            */
            temp = e.GetSecondVert(_obiekt);

            if (temp != null)
                Returned.Add(temp);
        }

        return Returned;
    }

    private void AddjustWeights(Obiekt _obiekt)
    {
        float AddedLenght;

        int _k_id, _o_id;

        _o_id = V.IndexOf(_obiekt);

        foreach (Obiekt _k in K)
        {
            AddedLenght = _baza.GetEdge(_obiekt, _k).GetLenght();
            
            _k_id = V.IndexOf(_k);

            if (W[_o_id] + AddedLenght < W[_k_id])
            {
                W[_o_id] = W[_o_id] + AddedLenght;
                P[_k_id] = _obiekt;
            } 
        }

        G.Add(_obiekt);
    }
}