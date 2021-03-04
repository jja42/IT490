using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Manager : MonoBehaviour
{
    string path;
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
        public int card_id;
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
        public Card(int card_id, string name, CardType card_type, Type type, int hp, string ability_name, string ability_description, string move_name_1, string move_description_1, int move_damage_1, string move_cost_1, string move_name_2, string move_description_2, int move_damage_2, string move_cost_2, string reposition_cost, string flavor_text)
        {
            this.card_id = card_id;
            this.name = name;
            this.card_type = card_type;
            this.type = type;
            this.hp = hp;
            this.ability_name = ability_name;
            this.ability_description = ability_description;
            this.move_name_1 = move_name_1;
            this.move_description_1 = move_description_1;
            this.move_damage_1 = move_damage_1;
            this.move_cost_1 = move_cost_1;
            this.move_name_2 = move_name_2;
            this.move_description_2 = move_description_2;
            this.move_damage_2 = move_damage_2;
            this.move_cost_2 = move_cost_2;
            this.reposition_cost = reposition_cost;
            this.flavor_text = flavor_text;
        }
    }
    public struct Deck
    {
        public int id;
        public List<Card> cards;
        public string name;
        public Deck(int id, string name)
        {
            this.id = id;
            this.cards = new List<Card>();
            this.name = name;
        }
    }
    public struct Hand
    {
        public List<Card> cards;
        //0 if player, 1 if opponent
        public int owner;
        public Hand(int owner)
        {
            this.owner = owner;
            this.cards = new List<Card>();
        }
    }
    public static Card_Manager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public Card GetCardByID(int card_id)
    {
        Card card = Database_Manager.instance.GetCard(card_id);
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
        for (int i = 0; i < deck.cards.Count; i++)
        {
            Card temp = deck.cards[i];
            int randomIndex = Random.Range(i, deck.cards.Count);
            deck.cards[i] = deck.cards[randomIndex];
            deck.cards[randomIndex] = temp;
        }

    }

    public void DrawfromDeck(Deck deck,Hand hand)
    {
        if (deck.cards.Count > 0) {
        hand.cards.Add(deck.cards[0]);
        path = "temp_prefabs/Card";
        GameObject obj = Resources.Load<GameObject>(path);
        Instantiate(obj, General_UI_Manager.instance.Player_Hand.transform);
        obj.GetComponent<Player_Input>().card_id = deck.cards[0].card_id;
        deck.cards.RemoveAt(0);
        General_UI_Manager.instance.ArrangeHand(hand);
        }
    }
    public void GenerateDeck(Deck deck)
    {
       for(int i = 0; i< Database_Manager.instance.Database.Count; i++)
        {
            deck.cards.Add(Database_Manager.instance.Database[i]);
        }
        ShuffleDeck(deck);
    }

}
