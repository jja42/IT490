using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Card_Manager.Deck player_deck;
    public Card_Manager.Hand player_hand;
    bool generated;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        generated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!generated)
        {
            player_deck = new Card_Manager.Deck(0,"player_deck");
            player_hand = new Card_Manager.Hand(0);
            Database_Manager.instance.GenerateDatabase();
            Card_Manager.instance.GenerateDeck(player_deck);
            generated = true;
        }
    }
    public void PopupCard(int card_id)
    {
        Card_UI_Manager.instance.PopupCard(card_id);
    }
    public void RemovePopup()
    {
        Card_UI_Manager.instance.RemovePopup();
    }
    public void GenerateDatabase()
    {
        Database_Manager.instance.GenerateDatabase();
    }
    public void DrawCard()
    {
        Card_Manager.instance.DrawfromDeck(player_deck,player_hand);
    }
}
