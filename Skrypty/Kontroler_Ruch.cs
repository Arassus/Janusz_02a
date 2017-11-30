using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kontroler_Ruch : MonoBehaviour
{
    public Zdarzenie.dProceduraMyszki
        RightMouseClickDown, RightMouseClick, RightMouseClickUp;

    public Zdarzenie.dProceduraKlawiszy
        BtnClick_W, BtnClickDown_W, BtnClickUp_W,
        BtnClick_S, BtnClickDown_S, BtnClickUp_S,
        BtnClick_A, BtnClickDown_A, BtnClickUp_A,
        BtnClick_D, BtnClickDown_D, BtnClickUp_D;

    RaycastHit _h;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        Update_Events();
	}

    public void Update_Events()
    {
        if (Input.GetMouseButtonDown(1))
            if (RightMouseClickDown != null)
                RightMouseClickDown(GetComponent<Collider>(), _h);

        if (Input.GetMouseButton(1))
            if (RightMouseClick != null)
                RightMouseClick(GetComponent<Collider>(), _h);

        if (Input.GetMouseButtonUp(1))
            if (RightMouseClickUp != null)
                RightMouseClickUp(GetComponent<Collider>(), _h);



        if (Input.GetKeyDown(KeyCode.W))
            if (BtnClickDown_W != null)
                BtnClickDown_W(GetComponent<Collider>());

        if (Input.GetKey(KeyCode.W))
            if (BtnClick_W != null)
                BtnClick_W(GetComponent<Collider>());

        if (Input.GetKeyUp(KeyCode.W))
            if (BtnClickUp_W != null)
                BtnClickUp_W(GetComponent<Collider>());



        if (Input.GetKeyDown(KeyCode.S))
            if (BtnClickDown_S != null)
                BtnClickDown_S(GetComponent<Collider>());

        if (Input.GetKey(KeyCode.S))
            if (BtnClick_S != null)
                BtnClick_S(GetComponent<Collider>());

        if (Input.GetKeyUp(KeyCode.S))
            if (BtnClickUp_S != null)
                BtnClickUp_S(GetComponent<Collider>());



        if (Input.GetKeyDown(KeyCode.A))
            if (BtnClickDown_A != null)
                BtnClickDown_A(GetComponent<Collider>());

        if (Input.GetKey(KeyCode.A))
            if (BtnClick_A != null)
                BtnClick_A(GetComponent<Collider>());

        if (Input.GetKeyUp(KeyCode.A))
            if (BtnClickUp_A != null)
                BtnClickUp_A(GetComponent<Collider>());



        if (Input.GetKeyDown(KeyCode.D))
            if (BtnClickDown_D != null)
                BtnClickDown_D(GetComponent<Collider>());

        if (Input.GetKey(KeyCode.D))
            if (BtnClick_D != null)
                BtnClick_D(GetComponent<Collider>());

        if (Input.GetKeyUp(KeyCode.D))
            if (BtnClickUp_D != null)
                BtnClickUp_D(GetComponent<Collider>());
    }
}
