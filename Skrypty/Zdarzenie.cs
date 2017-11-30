using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zdarzenie : MonoBehaviour
{
    public delegate void dProceduraKlawiszy(Collider sender);
    public delegate void dProceduraMyszki(Collider sender, RaycastHit mouseArgs);
    public delegate void dProceduraObiektu(GameObject sender);

    public static void InitDelegate(ref dProceduraMyszki Original, dProceduraMyszki Added)
    {
        if (Added != null)
            Original += Added;
    }
    public static void InitDelegate(ref dProceduraKlawiszy Original, dProceduraKlawiszy Added)
    {
        if (Added != null)
            Original += Added;
    }
    public static void InitDelegate(ref dProceduraObiektu Original, dProceduraObiektu Added)
    {
        if (Added != null)
            Original += Added;
    }
}
