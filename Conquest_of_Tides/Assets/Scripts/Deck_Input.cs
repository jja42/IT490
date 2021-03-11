using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Deck_Input : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int card_id;
    public Card_Manager.Card this_card;
    public bool in_deck;
    Image img;
    void Start()
    {
        this_card = Card_Manager.instance.GetCardByID(card_id);
        img = GetComponent<Image>();
        if (this_card.card_type == Card_Manager.CardType.Ship)
            img.sprite = Resources.Load<Sprite>("temp_assets/CardImg/" + this_card.card_id.ToString());
        if (this_card.card_type == Card_Manager.CardType.Fortification)
            img.sprite = Resources.Load<Sprite>("temp_assets/Fortification_card_" + (int)this_card.type);
    }
    public void OnClick()
    {
        if(!in_deck)
            Deck_Manager.instance.AddCard(card_id);
        else
        {
            Deck_Manager.instance.RemoveCard(this.gameObject,this.card_id);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Card_UI_Manager.instance.PopupCard(card_id);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Card_UI_Manager.instance.RemovePopup();
    }
}
