using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruch_Kamera : MonoBehaviour
{
    Kontroler_Ruch rKontroler;
    public float verticalSpeed = 1;
    public float horizontalSpeed = 1;

    public float Czulosc = 1;
    private float mnoznik;

    private float cos, sin;

    RaycastHit _h;
    void Start ()
    {
        rKontroler = GetComponent<Kontroler_Ruch>();

        mnoznik = Czulosc * Time.deltaTime;

        Init_Delegates();
	}
	
	void Update ()
    {
        rKontroler.Update_Events();
	}

    public void Init_Delegates()
    {
        Zdarzenie.InitDelegate(ref rKontroler.RightMouseClick, ViewRotation);
        Zdarzenie.InitDelegate(ref rKontroler.BtnClick_W, ViewTranslation_W);
        Zdarzenie.InitDelegate(ref rKontroler.BtnClick_S, ViewTranslation_S);
        Zdarzenie.InitDelegate(ref rKontroler.BtnClick_A, ViewTranslation_A);
        Zdarzenie.InitDelegate(ref rKontroler.BtnClick_D, ViewTranslation_D);
    }
    private void ViewRotation(Collider sender, RaycastHit mouseArgs)
    {
        mnoznik = Czulosc * Time.deltaTime;

        float x = horizontalSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        float y = verticalSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");

        transform.Rotate(-y, 0, 0, Space.Self);
        transform.Rotate(0, x, 0, Space.World);

        UpdateGeometry();
    }

    private void ViewTranslation_W(Collider sender)
    {
        transform.Translate(mnoznik * sin, 0, mnoznik * cos, Space.World);
    }
    private void ViewTranslation_S(Collider sender)
    {
        transform.Translate(-mnoznik * sin, 0, -mnoznik * cos, Space.World);
    }
    private void ViewTranslation_A(Collider sender)
    {
        transform.Translate(-mnoznik * cos, 0, -mnoznik * sin, Space.World);
    }
    private void ViewTranslation_D(Collider sender)
    {
        transform.Translate(mnoznik * cos, 0, mnoznik * sin, Space.World);
    }

    private void UpdateGeometry()
    {
        cos = Mathf.Cos(Mathf.PI * transform.rotation.eulerAngles.y / 180f);
        sin = Mathf.Sin(Mathf.PI * transform.rotation.eulerAngles.y / 180f);
    }
}
