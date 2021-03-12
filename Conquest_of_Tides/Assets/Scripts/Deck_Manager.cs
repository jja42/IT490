using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck_Manager : MonoBehaviour
{
    public static Deck_Manager instance;
    public Transform card_list;
    public Transform deck_list;
    public GameObject card_prefab;
    Vector3 list_offset;
    Vector3 x_offset;
    Vector3 y_offset;
    public Vector3 deck_offset;
    Vector3 deck_card_offset;
    public List<GameObject> deck;
    public List<int> deck_cards;
    public List<string> deck_name_list;
    GameObject obj;
    public List<int> player_cards;
    public Dropdown deck_names;
    public InputField deck_name;
    public void Start()
    {
        instance = this;
        deck_cards = new List<int>();
        deck = new List<GameObject>();
        list_offset = new Vector3(-275, 900, 0);
        x_offset = new Vector3(75, 0, 0);
        y_offset = new Vector3(0, -100, 0);
        deck_card_offset = new Vector3(-35, 320, 0);
        Database_Manager.instance.GenerateDatabase();
        //StartCoroutine(WebRequest.instance.GetDeckList(Settings_Manager.instance.user_id));
    }

    public void SpawnCard(int index)
    {
        obj = Instantiate(card_prefab, card_list);
        obj.GetComponent<Deck_Input>().card_id = player_cards[index];
        list_offset += x_offset;
        if (list_offset.x > 225)
        {
            list_offset -= (x_offset * 6);
            list_offset += y_offset;
        }
        obj.transform.position += list_offset;
    }
    public void AddCard(int card_id)
    {
        deck_cards.Add(card_id);
        obj = Instantiate(card_prefab, deck_list);
        obj.GetComponent<Deck_Input>().card_id = card_id;
        obj.GetComponent<Deck_Input>().in_deck = true;
        deck.Add(obj);
        ArrangeDeck();
    }
    public void LoadCard(int card_id)
    {
        obj = Instantiate(card_prefab, deck_list);
        obj.GetComponent<Deck_Input>().card_id = card_id;
        obj.GetComponent<Deck_Input>().in_deck = true;
        deck.Add(obj);
        ArrangeDeck();
    }
    public void ArrangeDeck()
    {
        deck_offset = new Vector3(0, 0, 0);
        for (int index = 0; index < deck_list.transform.childCount; index++)
        {
            deck[index].transform.position = deck_card_offset;
        }
        for (int index = 0; index < deck_list.transform.childCount; index++)
        {
            deck_offset += x_offset;
            if (deck_offset.x > 400)
            {
                deck_offset -= (x_offset * 5);
                deck_offset += y_offset;
            }
            deck[index].transform.position += deck_offset;
        }
    }
    public void GetCards()
    {
        StartCoroutine(WebRequest.instance.GetCards(Settings_Manager.instance.user_id));
    }
    public void GetDeck()
    {
        StartCoroutine(WebRequest.instance.GetDeck(deck_names.options[deck_names.value].text,Settings_Manager.instance.user_id));
    }
    public void LoadDeck()
    {
        for(int i =0; i < deck_cards.Count; i++)
        {
            LoadCard(deck_cards[i]);
        }
    }
    public void RemoveCard(GameObject card_obj,int card_id)
    {
        deck_cards.Remove(card_id);
        deck.Remove(card_obj);
        Destroy(card_obj);
    }
    public void SetDeckNames()
    {
        deck_names.ClearOptions();
        deck_names.AddOptions(deck_name_list);
    }
    public void SaveDeck()
    {
        if (string.IsNullOrEmpty(deck_name.text) && deck_cards.Count!=60)
        {
            return;
        }
        StartCoroutine(WebRequest.instance.SendDeck(deck_cards, deck_name.text,Settings_Manager.instance.user_id));
    }
}
