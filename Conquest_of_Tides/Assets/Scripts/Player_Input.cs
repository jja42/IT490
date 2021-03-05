using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public bool hovering;
    public int card_id;
    public Card_Manager.Card this_card;
    public bool active;
    public bool bench;
    public bool attached;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        this_card = Card_Manager.instance.GetCardByID(card_id);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(this_card.card_type == Card_Manager.CardType.Ship)
            spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/Ship_type_" + (int)this_card.type);
            //spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/" + this_card.card_id.ToString());
        if (this_card.card_type == Card_Manager.CardType.Fortification)
            spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/Fortification_card_" + (int)this_card.type);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveWithMouse();
        if (hovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (this_card.card_type == Card_Manager.CardType.Ship)
                {
                    if (!GameManager.instance.active_set && !active && !bench)
                    {
                        GameManager.instance.SetActiveZone(this.gameObject);
                        active = true;
                    }
                    else
                    {
                        if (!GameManager.instance.full_bench && !active && !bench)
                        {
                            GameManager.instance.SetBench(this.gameObject);
                            bench = true;
                        }
                    }
                    if (GameManager.instance.selecting && (bench||active))
                    {
                        GameManager.instance.Selected_Ship = this.gameObject;
                    }
                }
                if(this_card.card_type == Card_Manager.CardType.Fortification)
                {
                    if (!GameManager.instance.selecting && !attached)
                    {
                        GameManager.instance.selecting = true;
                        General_UI_Manager.instance.EnableAttachmentUI();
                        GameManager.instance.Selected_Fortification = this.gameObject;
                        attached = true;
                    }
                }
            }
        }
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
