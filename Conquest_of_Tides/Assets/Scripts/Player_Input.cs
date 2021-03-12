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
    public string attached_fortifications;
    public int owner;
    public bool generated;
    public int damage_taken;

    // Update is called once per frame
    void Update()
    {
        if (!generated)
        {
            this_card = Card_Manager.instance.GetCardByID(card_id);
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (owner != 0)
            {
                if (this_card.card_type == Card_Manager.CardType.Ship)
                    spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/CardImg/" + this_card.card_id.ToString());
                if (this_card.card_type == Card_Manager.CardType.Fortification)
                    spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/Fortification_card_" + (int)this_card.type);
            }
            else
            {
                spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/generic");
            }
            attached_fortifications = "";
            generated = true;
        }
        if (!GameManager.instance.paused)
        {
            if (hovering && Turn_Manager.instance.currState == Turn_Manager.TurnState.Main)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (this_card.card_type == Card_Manager.CardType.Ship)
                    {
                        if (active && GameManager.instance.can_retreat && !GameManager.instance.repositioning && !GameManager.instance.attaching)
                        {
                            General_UI_Manager.instance.EnableRepositionUI();
                            GameManager.instance.Selection_1 = this.gameObject;
                            GameManager.instance.repositioning = true;
                        }
                        if (GameManager.instance.repositioning && bench)
                        {
                            GameManager.instance.Selection_2 = this.gameObject;
                        }
                        if (!GameManager.instance.active_set && !active && !bench)
                        {
                            PlayerTurnManager.instance.SetActive(card_id);
                            GameManager.instance.SetPlayerActiveZone(this.gameObject);
                            active = true;
                            GameManager.instance.player_hand.cards.Remove(this_card);
                        }
                        else
                        {
                            if (!GameManager.instance.full_bench && !active && !bench)
                            {
                                PlayerTurnManager.instance.SetBench(card_id);
                                GameManager.instance.SetPlayerBench(this.gameObject);
                                bench = true;
                                GameManager.instance.player_hand.cards.Remove(this_card);
                            }
                        }
                        if (GameManager.instance.attaching && (bench || active))
                        {
                            GameManager.instance.Selection_2 = this.gameObject;
                        }
                    }
                    if (this_card.card_type == Card_Manager.CardType.Fortification)
                    {
                        if (!GameManager.instance.attaching && !attached && GameManager.instance.can_attach)
                        {
                            GameManager.instance.attaching = true;
                            General_UI_Manager.instance.EnableAttachmentUI();
                            GameManager.instance.Selection_1 = this.gameObject;
                            attached = true;
                            GameManager.instance.player_hand.cards.Remove(this_card);
                        }
                    }
                }
            }
            if (hovering && Turn_Manager.instance.currState == Turn_Manager.TurnState.Combat)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (GameManager.instance.can_attack && active)
                    {
                        GameManager.instance.AttackInitiate(this_card);
                    }
                }
            }
        }
    }
    private void OnMouseEnter()
    {
        if (!GameManager.instance.paused && owner != 0)
        { 
            hovering = true;
            GameManager.instance.PopupCard(card_id);
        }
    }
    private void OnMouseExit()
    {
        hovering = false;
        GameManager.instance.RemovePopup();
    }
    public void Reveal()
    {
        owner = 2;
        this_card = Card_Manager.instance.GetCardByID(card_id);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (this_card.card_type == Card_Manager.CardType.Ship)
            spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/CardImg/" + this_card.card_id.ToString());
        if (this_card.card_type == Card_Manager.CardType.Fortification)
            spriteRenderer.sprite = Resources.Load<Sprite>("temp_assets/Fortification_card_" + (int)this_card.type);
    }
}
