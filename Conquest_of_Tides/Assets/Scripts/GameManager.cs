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
    public GameObject Selection_1;
    public GameObject Selection_2;
    public bool attaching;
    public bool repositioning;
    public bool can_draw;
    public bool can_attach;
    public bool can_retreat;
    public bool can_attack;
    string str;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        generated = false;
        active_set = false;
        full_bench = false;
        bench_count = 0;
        attaching = false;
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
        if (attaching)
        {
            if(Selection_1 != null && Selection_2 != null)
            {
                AttachFortification(Selection_1,Selection_2);
                attaching = false;
                Selection_1 = null;
                Selection_2 = null;
            }
        }
        if (repositioning)
        {
            if (Selection_1 != null && Selection_2 != null)
            {
                Reposition(Selection_1, Selection_2);
                repositioning = false;
                Selection_1 = null;
                Selection_2 = null;
            }
        }
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Start)
        {
            for (int i = 0; i < 7; i++)
            {
                DrawCard();
            }
            can_attach = true;
            can_retreat = true;
            Turn_Manager.instance.currState = Turn_Manager.TurnState.Main;
        }
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Draw)
        {
            can_attach = true;
            can_retreat = true;
        }
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Main)
        {
            can_attack = true;
        }
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.End)
        {
            can_draw = true;
            Turn_Manager.instance.currState = Turn_Manager.TurnState.Draw;
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
        if(Turn_Manager.instance.currState == Turn_Manager.TurnState.Draw)
            Turn_Manager.instance.currState = Turn_Manager.TurnState.Main;
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
        can_attach = false;
        str = "" + (int)Fortification.GetComponent<Player_Input>().this_card.type;
        Ship.GetComponent<Player_Input>().attached_fortifications += str;
        General_UI_Manager.instance.AttachFortification(Fortification,Ship);
    }
    public void DeckButton()
    {
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Draw)
        {
            DrawCard();
            return;
        }
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Main)
        {
            Turn_Manager.instance.currState = Turn_Manager.TurnState.Combat;
            return;
        }
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Combat)
        {
            Turn_Manager.instance.currState = Turn_Manager.TurnState.End;
            return;
        }
    }
    public void AttackInitiate(Card_Manager.Card card)
    {
        General_UI_Manager.instance.EnableAttack_UI(card);
    }
    public void Reposition(GameObject ActiveShip, GameObject BenchShip)
    {
        can_retreat = false;
        General_UI_Manager.instance.Reposition(ActiveShip, BenchShip);
    }
    public void AttackResolve()
    {

    }
}
