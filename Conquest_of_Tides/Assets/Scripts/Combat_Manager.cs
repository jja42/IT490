using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Manager : MonoBehaviour
{
    public static Combat_Manager instance;
    public GameObject player_active;
    public GameObject opponent_active;
    public Card_Manager.Card player_card;
    public Card_Manager.Card opponent_card;
    public GameObject ten_counter;
    public GameObject thirty_counter;
    public GameObject fifty_counter;
    public GameObject miss_ui;
    public GameObject storm_ui;
    public Text player_active_damage_ui;
    public Text opponent_active_damage_ui;
    public int damage;
    string[] move_cost;
    int hp_player;
    int hp_opponent;
    string current_fortifications;
    string[] fortifications;
    public GameObject Player_Active_Zone;
    public GameObject Opponent_Active_Zone;
    public GameObject Victory_UI;
    public GameObject Loss_UI;
    public void Awake()
    {
        instance = this;
    }

    public void HandlePlayerCombat()
    {
        if (Player_Active_Zone.transform.childCount > 0 && Opponent_Active_Zone.transform.childCount > 0)
        {
            player_active = Player_Active_Zone.transform.GetChild(1).gameObject;
            opponent_active = Opponent_Active_Zone.transform.GetChild(1).gameObject;
            player_card = player_active.GetComponent<Player_Input>().this_card;
            opponent_card = opponent_active.GetComponent<Player_Input>().this_card;
            current_fortifications = player_active.GetComponent<Player_Input>().attached_fortifications;
            PlayerAttack(player_card, opponent_card);
        }
    }
    public void HandleOpponentCombat(int damage)
    {
            player_active = Player_Active_Zone.transform.GetChild(1).gameObject;
            opponent_active = Opponent_Active_Zone.transform.GetChild(1).gameObject;
            player_card = player_active.GetComponent<Player_Input>().this_card;
            opponent_card = opponent_active.GetComponent<Player_Input>().this_card;
            OpponentAttack(player_card, opponent_card, damage);
    }
    public void PlayerAttack(Card_Manager.Card player_card, Card_Manager.Card opponent_card)
    {
        damage = player_card.move_damage_1;
        if(Weather_Manager.instance.move_damage_reduction > 0)
        {
            damage -= Weather_Manager.instance.move_damage_reduction;
        }
        if(player_card.type == Card_Manager.instance.Get_Weakness(opponent_card.type))
        {
            damage += 20;
            if (Weather_Manager.instance.weakness_enhancement > 0)
                damage += Weather_Manager.instance.weakness_enhancement;
        }
        if (player_card.type == Card_Manager.instance.Get_Resistance(opponent_card.type))
        {
            damage -= 20;
            if (Weather_Manager.instance.resistance_reduction > 0)
                damage += Weather_Manager.instance.resistance_reduction;
        }
        if (damage < 0)
            damage = 0;
        move_cost = player_card.move_cost_1.Split('-');
        fortifications = current_fortifications.Split('-');
        for (int i = 0; i < move_cost.Length; i++)
        {
            if (move_cost[i] == "0")
            {
                continue;
            }
            if (fortifications[i] == null)
            {
                General_UI_Manager.instance.Attack_UI.SetActive(true);
                General_UI_Manager.instance.Attack_Text.text = "You do not have\n the necessary\n Fortifications to\n use that move.";
                return;
            }
            if (Weather_Manager.instance.typeless_cost)
                move_cost[i] = "7";
            if (fortifications[i] == move_cost[i] || (fortifications[i] != "0" && move_cost[i] == "7"))
                continue;
            else
            {
                General_UI_Manager.instance.Attack_UI.SetActive(true);
                General_UI_Manager.instance.Attack_Text.text = "You do not have\n the necessary\n Fortifications to\n use that move.";
                return;
            }
        }
        General_UI_Manager.instance.Attack_UI.SetActive(false);
        GameManager.instance.can_attack = false;
        if (Random.Range(0, 100) > Weather_Manager.instance.w_visibility)
        {
            miss_ui.SetActive(true);
        }
        else
        {
            opponent_active.GetComponent<Player_Input>().damage_taken += damage;
            PlayerTurnManager.instance.Combat(damage);
            if (Weather_Manager.instance.decreased_hp > 0)
            {
                if (opponent_active.GetComponent<Player_Input>().damage_taken >= (opponent_card.hp - Weather_Manager.instance.decreased_hp))
                {
                    print(damage);
                    print(opponent_card.hp);
                    Destroy(opponent_active);
                    PrizeCard();
                    opponent_active_damage_ui.text = "Opponent Ship HP: ";
                }
            }
            else
            {
                if (opponent_active.GetComponent<Player_Input>().damage_taken >= opponent_card.hp)
                {
                    print(damage);
                    print(opponent_card.hp);
                    Destroy(opponent_active);
                    PrizeCard();
                    opponent_active_damage_ui.text = "Opponent Ship HP: ";
                }
            }
        }
            player_active_damage_ui.text = "Player Ship HP: " + (player_card.hp - Weather_Manager.instance.decreased_hp - player_active.GetComponent<Player_Input>().damage_taken).ToString();
            opponent_active_damage_ui.text = "Opponent Ship HP: " + (opponent_card.hp - Weather_Manager.instance.decreased_hp - opponent_active.GetComponent<Player_Input>().damage_taken).ToString();
    }
    public void OpponentAttack(Card_Manager.Card player_card, Card_Manager.Card opponent_card, int damage)
    {
            player_active.GetComponent<Player_Input>().damage_taken += damage;
            if (Weather_Manager.instance.decreased_hp > 0)
            {
                if (player_active.GetComponent<Player_Input>().damage_taken >= (opponent_card.hp - Weather_Manager.instance.decreased_hp))
                {
                    Destroy(player_active);
                    Lose();
                    PlayerTurnManager.instance.Match(Settings_Manager.instance.username);
                    player_active_damage_ui.text = "Player Ship HP: ";
                }
            }
            else
            {
                if (player_active.GetComponent<Player_Input>().damage_taken >= opponent_card.hp)
                {
                Destroy(player_active);
                Lose();
                PlayerTurnManager.instance.Match(Settings_Manager.instance.username);
                player_active_damage_ui.text = "Player Ship HP: ";
                }
            }
        player_active_damage_ui.text = "Player Ship HP: " + (player_card.hp - Weather_Manager.instance.decreased_hp - player_active.GetComponent<Player_Input>().damage_taken).ToString();
        opponent_active_damage_ui.text = "Opponent Ship HP: " + (opponent_card.hp - Weather_Manager.instance.decreased_hp - opponent_active.GetComponent<Player_Input>().damage_taken).ToString();
    }
    public void PrizeCard()
    {
        GameManager.instance.paused = true;
        Victory_UI.SetActive(true);
    }
    public void Lose()
    {
        GameManager.instance.paused = true;
        Loss_UI.SetActive(true);
    }
    public void ArrangeCounters(GameObject obj)
    {
        Vector3 offset = new Vector3(0, 0, 0);
        for(int i = 1; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).position += offset;
        }
    }
    public void DisableMissUI()
    {
        miss_ui.SetActive(false);
    }
    public void DisableStormUI()
    {
        storm_ui.SetActive(false);
    }
    public void Storm()
    {
        if (Player_Active_Zone.transform.childCount > 1)
        {
            player_active = Player_Active_Zone.transform.GetChild(1).gameObject;
            player_card = player_active.GetComponent<Player_Input>().this_card;
            if (Weather_Manager.instance.turn_damage > 0)
            {
                player_active.GetComponent<Player_Input>().damage_taken += Weather_Manager.instance.turn_damage;
                if (Weather_Manager.instance.decreased_hp > 0)
                {
                    if (player_active.GetComponent<Player_Input>().damage_taken >= (player_card.hp - Weather_Manager.instance.decreased_hp))
                    {
                        Destroy(player_active);
                        player_active_damage_ui.text = "Player Ship HP: ";
                    }
                }
                else
                {
                    if (player_active.GetComponent<Player_Input>().damage_taken >= player_card.hp)
                    {
                        Destroy(player_active);
                        player_active_damage_ui.text = "Player Ship HP: ";
                    }
                }
                storm_ui.SetActive(true);
            }
            player_active_damage_ui.text = "Player Ship HP: " + (player_card.hp - Weather_Manager.instance.decreased_hp - player_active.GetComponent<Player_Input>().damage_taken).ToString();
        }
        if (Opponent_Active_Zone.transform.childCount > 1)
        {
            opponent_active = Opponent_Active_Zone.transform.GetChild(1).gameObject;
            opponent_card = opponent_active.GetComponent<Player_Input>().this_card;
            if (Weather_Manager.instance.turn_damage > 0)
            {
                opponent_active.GetComponent<Player_Input>().damage_taken += Weather_Manager.instance.turn_damage;
                if (Weather_Manager.instance.decreased_hp > 0)
                {
                    if (opponent_active.GetComponent<Player_Input>().damage_taken >= (opponent_card.hp - Weather_Manager.instance.decreased_hp))
                    {
                        Destroy(opponent_active);
                        opponent_active_damage_ui.text = "Opponent Ship HP: ";
                    }
                }
                else
                {
                    if (opponent_active.GetComponent<Player_Input>().damage_taken >= opponent_card.hp)
                    {
                        Destroy(opponent_active);
                        opponent_active_damage_ui.text = "Opponent Ship HP: ";
                    }
                }
                storm_ui.SetActive(true);
            }
            opponent_active_damage_ui.text = "Opponent Ship HP: " + (opponent_card.hp - Weather_Manager.instance.decreased_hp - opponent_active.GetComponent<Player_Input>().damage_taken).ToString();
        }
        
    }
}
