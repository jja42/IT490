using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Card_Manager.Deck player_deck;
    public Card_Manager.Hand player_hand;
    public Card_Manager.Deck opponent_deck;
    public Card_Manager.Hand opponent_hand;
    bool generated;
    public bool active_set;
    public bool full_bench;
    public int opponent_bench_count;
    public int player_bench_count;
    public GameObject Selection_1;
    public GameObject Selection_2;
    public bool attaching;
    public bool repositioning;
    public bool can_draw;
    public bool can_attach;
    public bool can_retreat;
    public bool can_attack;
    string str;
    public bool paused;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        generated = false;
        active_set = false;
        full_bench = false;
        player_bench_count = 0;
        opponent_bench_count = 0;
        attaching = false;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!generated)
        {
            player_deck = new Card_Manager.Deck(1,"player_deck");
            player_hand = new Card_Manager.Hand(1);
            opponent_deck = new Card_Manager.Deck(2,"opponent_deck");
            opponent_hand = new Card_Manager.Hand(2);
            Database_Manager.instance.GenerateDatabase();
            if (Settings_Manager.instance.demo)
            {
                Database_Manager.instance.GenerateTestDeck();
            }
            Card_Manager.instance.GenerateDeck(player_deck);
            Card_Manager.instance.GenerateDeck(opponent_deck);
            generated = true;
            if (!Settings_Manager.instance.demo)
            {
                if (!Settings_Manager.instance.historical)
                    Weather_Manager.instance.GetData();
                else
                {
                    Weather_Manager.instance.GetHistoricalData(Settings_Manager.instance.historical_date);
                }
            }
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
                PlayerReposition(Selection_1, Selection_2);
                repositioning = false;
                Selection_1 = null;
                Selection_2 = null;
            }
        }
        if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Start)
        {
            if (!Settings_Manager.instance.tutorial)
            {
                for (int i = 0; i < 7; i++)
                {
                    DrawCard();
                }
            }
            else
            {
                Card_Manager.instance.AddtoHand(player_hand, "Ironclad_1");
                Card_Manager.instance.AddtoHand(player_hand, "Ironclad_Fortification");
                Card_Manager.instance.AddtoHand(player_hand, "Galleon_1");
                General_UI_Manager.instance.ArrangePlayerHand(player_hand);
            }
            OpponentStart();
            can_attach = true;
            can_retreat = true;
            Turn_Manager.instance.currState = Turn_Manager.TurnState.Main;
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
        Card_Manager.instance.DrawfromPlayerDeck(player_deck,player_hand);
        General_UI_Manager.instance.ArrangePlayerHand(player_hand);
    }
    public void DrawOpponentCard() {
        Card_Manager.instance.DrawfromOpponentDeck(opponent_deck, opponent_hand);
        General_UI_Manager.instance.ArrangeOpponentHand(opponent_hand);
    }
    public void SetPlayerActiveZone(GameObject obj)
    {
        active_set = true;
        General_UI_Manager.instance.MoveToPlayerActiveZone(obj);
    }
    public void SetOpponentActiveZone(int id)
    {
        General_UI_Manager.instance.MoveToOpponentActiveZone(id);
    }
    public void SetPlayerBench(GameObject obj)
    {
        player_bench_count++;
        if(player_bench_count > 5)
        {
            full_bench = true;
        }
        else
        {
            General_UI_Manager.instance.MoveToPlayerBench(obj);
        }
    }

    public void SetOpponentBench(int id)
    {
        opponent_bench_count++;
        General_UI_Manager.instance.MoveToOpponentBench(id);
    }

    public void AttachFortification(GameObject Fortification, GameObject Ship)
    {
        can_attach = false;
        if (Ship.GetComponent<Player_Input>().attached_fortifications == "")
            str = ((int)Fortification.GetComponent<Player_Input>().this_card.type).ToString();
        else
        {
            str = "-" + (int)Fortification.GetComponent<Player_Input>().this_card.type;
        }
        if (Weather_Manager.instance.double_fortify)
            str += "-" + (int)Fortification.GetComponent<Player_Input>().this_card.type;
        Ship.GetComponent<Player_Input>().attached_fortifications += str;
        if(Ship.GetComponent<Player_Input>().active)
            if (!Settings_Manager.instance.demo)
                PlayerTurnManager.instance.AttachFortificationActive(Fortification.GetComponent<Player_Input>().card_id);
        if (Ship.GetComponent<Player_Input>().bench)
        {
            int index = Ship.transform.GetSiblingIndex();
            if (!Settings_Manager.instance.demo)
                PlayerTurnManager.instance.AttachFortificationBench(Fortification.GetComponent<Player_Input>().card_id,index);
        }
        General_UI_Manager.instance.AttachFortification(Fortification,Ship);
    }
    public void DeckButton()
    {
        if (!Settings_Manager.instance.demo)
        {
            if (!paused && (int)Turn_Manager.instance.currPlayer == PlayerTurnManager.instance.turn_id)
            {
                if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Draw)
                {
                    can_attach = true;
                    can_retreat = true;
                    if (Weather_Manager.instance.double_draw)
                    {
                        DrawCard();
                        DrawCard();
                        Turn_Manager.instance.currState = Turn_Manager.TurnState.Main;
                        Turn_Manager.instance.SetState(2);
                        PlayerTurnManager.instance.SetTurnPhase(2);
                        return;
                    }
                    DrawCard();
                    Turn_Manager.instance.currState = Turn_Manager.TurnState.Main;
                    Turn_Manager.instance.SetState(2);
                    PlayerTurnManager.instance.SetTurnPhase(2);
                    return;
                }
                if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Main)
                {
                    can_attack = true;
                    Turn_Manager.instance.currState = Turn_Manager.TurnState.Combat;
                    Turn_Manager.instance.SetState(3);
                    PlayerTurnManager.instance.SetTurnPhase(3);
                    return;
                }
                if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Combat)
                {
                    Turn_Manager.instance.currState = Turn_Manager.TurnState.End;
                    Turn_Manager.instance.SetState(4);
                    PlayerTurnManager.instance.SetTurnPhase(4);
                    return;
                }
                if (Turn_Manager.instance.currState == Turn_Manager.TurnState.End)
                {
                    can_draw = true;
                    Combat_Manager.instance.Storm();
                    Turn_Manager.instance.currState = Turn_Manager.TurnState.Draw;
                    Turn_Manager.instance.SetState(1);
                    PlayerTurnManager.instance.SetTurnPhase(1);
                    Turn_Manager.instance.SwitchTurn();
                }
            }
        }
        else
        {
            if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Draw)
            {
                can_attach = true;
                can_retreat = true;
                if (Weather_Manager.instance.double_draw)
                {
                    DrawCard();
                    DrawCard();
                    Turn_Manager.instance.currState = Turn_Manager.TurnState.Main;
                    Turn_Manager.instance.SetState(2);
                    return;
                }
                DrawCard();
                Turn_Manager.instance.currState = Turn_Manager.TurnState.Main;
                Turn_Manager.instance.SetState(2);
                return;
            }
            if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Main)
            {
                can_attack = true;
                Turn_Manager.instance.currState = Turn_Manager.TurnState.Combat;
                Turn_Manager.instance.SetState(3);
                return;
            }
            if (Turn_Manager.instance.currState == Turn_Manager.TurnState.Combat)
            {
                Turn_Manager.instance.currState = Turn_Manager.TurnState.End;
                Turn_Manager.instance.SetState(4);
                return;
            }
            if (Turn_Manager.instance.currState == Turn_Manager.TurnState.End)
            {
                can_draw = true;
                Combat_Manager.instance.Storm();
                Turn_Manager.instance.currState = Turn_Manager.TurnState.Draw;
                Turn_Manager.instance.SetState(1);
            }
        }
    }
    public void AttackInitiate(Card_Manager.Card card)
    {
        General_UI_Manager.instance.EnableAttack_UI(card);
    }
    public void PlayerReposition(GameObject ActiveShip, GameObject BenchShip)
    {
        can_retreat = false;
        int index = BenchShip.transform.GetSiblingIndex();
        if (!Settings_Manager.instance.demo)
            PlayerTurnManager.instance.RepositionShips(index);
        General_UI_Manager.instance.PlayerReposition(ActiveShip, BenchShip);
    }
    public void PlayerAttackResolve()
    {
        Combat_Manager.instance.HandlePlayerCombat();
    }
    public void OpponentStart()
    {
        for (int i = 0; i < 7; i++)
        {
            DrawOpponentCard();
        }
    }
    public void Pause()
    {
        if ((int)Turn_Manager.instance.currPlayer != PlayerTurnManager.instance.turn_id)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }
}
