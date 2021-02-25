using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public bool hovering;
    public int card_id;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //MoveWithMouse();
    }
    private void OnMouseEnter()
    {
        hovering = true;
        GameManager.instance.PopupCard(card_id);
    }
    private void OnMouseExit()
    {
        hovering = false;
        GameManager.instance.RemovePopup();
    }
    private void MoveWithMouse()
    {
        if (Input.GetMouseButton(0) && hovering)
        {
            this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            print(transform.position);
        }
    }

}
