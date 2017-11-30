using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Obiekt _obiekt;

    public Renderer rMesh;
    public Texture2D ClickDown, Click, ClickUp, Hover, Neutral;

    public Texture2D Miniatura;
    public string Napis;

    void Start()
    {
        rMesh = GetComponent<Renderer>();
        SetCurrentTexture(Neutral);

        _obiekt = GetComponent<Obiekt>();

        if (_obiekt != null)
        {
            _obiekt.Init_Delegates(Lewy_Click, Lewy_ClickDown, Lewy_ClickUp, null, null, null, Mysz_Hover, Mysz_Leave, null, null, null, null);
        }
    }

    void Update()
    {

    }

    private void Lewy_ClickDown(Collider sender, RaycastHit mouseArgs)
    {
        SetCurrentTexture(ClickDown);
    }
    private void Lewy_Click(Collider sender, RaycastHit mouseArgs)
    {
        SetCurrentTexture(Click);
    }
    private void Lewy_ClickUp(Collider sender, RaycastHit mouseArgs)
    {
        SetCurrentTexture(ClickUp);
    }
    private void Mysz_Hover(Collider sender, RaycastHit mouseArgs)
    {
        SetCurrentTexture(Hover);
    }
    private void Mysz_Leave(Collider sender, RaycastHit mouseArgs)
    {
        SetCurrentTexture(Neutral);
    }


    public void SetCurrentTexture(Texture2D NewTexture)
    {
        if (rMesh != null)
        {
            rMesh.material.mainTexture = NewTexture;
        }
    }
}
