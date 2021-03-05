using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Card_Manager.Deck player_deck;
    public Card_Manager.Hand player_hand;
    bool generated;
    public bool active_set;
    public bool full_bench;
    public int bench_count;
    public GameObject Selected_Fortification;
    public GameObject Selected_Ship;
    public bool selecting;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        generated = false;
        active_set = false;
        full_bench = false;
        bench_count = 0;
        selecting = false;
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
            for (int i = 0; i < 7; i++)
            {
                DrawCard();
            }
        }
        if (selecting)
        {
            if(Selected_Fortification != null && Selected_Ship != null)
            {
                AttachFortification(Selected_Fortification,Selected_Ship);
                selecting = false;
                Selected_Fortification = null;
                Selected_Ship = null;
            }
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
    public void SetActiveZone(GameObject obj)
    {
        active_set = true;
        General_UI_Manager.instance.MoveToActiveZone(obj);
    }
    public void SetBench(GameObject obj)
    {
        bench_count++;
        if(bench_count > 5)
        {
            full_bench = true;
        }
        else
        {
            General_UI_Manager.instance.MoveToBench(obj);
        }
    }
    public void AttachFortification(GameObject Fortification, GameObject Ship)
    {
        General_UI_Manager.instance.AttachFortification(Fortification,Ship);
    }
}
