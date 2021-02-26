using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Manager : MonoBehaviour
{

    public enum Type:int{
        None = 0,
        Ironclad = 1,
        Battleship = 2,
        Galleon = 3,
        Frigate = 4,
        Destroyer = 5,
        Submarine = 6,
        Gunboat = 7,
        Cruiser = 8,
        Dreadnought = 9,
        Aircraft_Carrier = 10,
        Corvette = 11
    }

    public enum CardType:int
    {
        Ship = 1,
        Reinforcement = 2,
        Fortification = 3
    }
    public struct Card
    {
        public int id;
        public string name;
        public CardType card_type;
        public Type type;
        public int hp;
        public string ability_name;
        public string ability_description;
        public string move_name_1;
        public string move_description_1;
        public int move_damage_1;
        public string move_cost_1;
        public string move_name_2;
        public string move_description_2;
        public int move_damage_2;
        public string move_cost_2;
        public string reposition_cost;
        public string flavor_text;
        public Card(int card_id)
        {
            id = card_id;
            name = "test";
            card_type = CardType.Reinforcement;
            type = Type.Galleon;
            hp = 100;
            ability_name = "test_ability";
            ability_description = "Example Description for Ability";
            move_name_1 = "test move # 1";
            move_description_1 = "Example Description Reinforcement";
            move_damage_1 = 40;
            move_cost_1 = "1120";
            move_name_2 = "test move # 2";
            move_description_2 = "Example Description for Move 2";
            move_damage_2 = 80;
            move_cost_2 = "1500";
            reposition_cost = "1100";
            flavor_text = "Example Card LMAO";
        }
    }
    public struct Deck
    {
        public int id;
        public List<Card> cards;
        public string name;
    }
    public struct Hand
    {
        public List<Card> cards;
        //0 if player, 1 if opponent
        public int owner;
    }
    public static Card_Manager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Card GetCardByID(int card_id)
    {
        Card card = new Card(card_id);
        return card;
    }
    public Type Get_Weakness(Type type)
    {
        switch (type)
        {
            case Type.Ironclad:
                return Type.Battleship;
            case Type.Battleship:
                return Type.Galleon;
            case Type.Galleon:
                return Type.Ironclad;
            case Type.Frigate:
                return Type.Submarine;
            case Type.Destroyer:
                return Type.Cruiser;
            case Type.Submarine:
                return Type.Destroyer;
            case Type.Gunboat:
                return Type.Submarine;
            case Type.Cruiser:
                return Type.Submarine;
            case Type.Dreadnought:
                return Type.Battleship;
            case Type.Aircraft_Carrier:
                return Type.Corvette;
            case Type.Corvette:
                return Type.Dreadnought;
        }
        return Type.None;
    }
    public Type Get_Resistance(Type type)
    {
        switch (type)
        {
            case Type.Frigate:
                return Type.Dreadnought;
            case Type.Cruiser:
                return Type.Destroyer;
            case Type.Dreadnought:
                return Type.Destroyer;
            case Type.Corvette:
                return Type.Cruiser;
        }
        return Type.None;
    }
    public void ShuffleDeck(Deck deck)
    {

    }

    public void DrawfromDeck(Deck deck,Hand hand)
    {
        if (deck.cards.Count > 0) { 
        int index = hand.cards.Count;
        hand.cards[index] = deck.cards[0];
        deck.cards.RemoveAt(0);
        General_UI_Manager.instance.ArrangeHand(hand);
        }
    }
}
