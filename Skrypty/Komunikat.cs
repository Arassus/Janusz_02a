using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Komunikat : MonoBehaviour
{
    public Ray _r;
    public RaycastHit _h;

    Text _text;

    Vector3 Pos;

	void Start ()
    {
        _text = GetComponent<Text>();
	}

    void Update()
    {
        _r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_r, out _h))
        {
            if (_h.collider.gameObject.GetComponent<Obiekt>() != null)
            {
                Pos = new Vector3(_h.point.x + 2.5f,
                  _h.point.y + 0.01f,
                  _h.point.z - 2.5f);

                transform.position = Pos;

                if (!_text.enabled)
                    _text.enabled = true;
            }
            else
            {
                if (_text.enabled)
                    _text.enabled = false;
            }
        }
    }

    public void WyswietlKomunikat(string NowyKomunikat)
    {
        _text.text = NowyKomunikat;
    }

}
