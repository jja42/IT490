using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck_Manager : MonoBehaviour
{
    public static Deck_Manager instance;
    public Transform deck_list;
    public GameObject card_prefab;
    float x_offset;
    float y_offset;
    Vector2 deck_offset;
    public List<GameObject> deck;
    public List<int> deck_cards;
    public Dictionary<int,int> cardDict;
    public List<string> deck_name_list;
    GameObject obj;
    public Dropdown deck_names;
    public InputField deck_name;
    public GameObject ImportExportMenu;
    public InputField exportField;
    public InputField importField;
    public void Start()
    {
        instance = this;
        deck_cards = new List<int>();
        deck = new List<GameObject>();
        x_offset = 160;
        y_offset =-210;
        Database_Manager.instance.GenerateDatabase();
        //StartCoroutine(WebRequest.instance.GetDeckList(Settings_Manager.instance.user_id, true));
        cardDict = new Dictionary<int, int>();
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
    }
    public void ArrangeDeck()
    {
        for (int index = 0; index < deck.Count; index++)
        {
            RectTransform rectTransform = deck[index].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(-320, 1050, 0);
            deck_offset = new Vector2(x_offset, 0);
            deck_offset *= index;
            while (deck_offset.x > 640)
            {
                deck_offset.x -= (x_offset * 5);
                deck_offset.y += y_offset;
            }
           rectTransform.anchoredPosition += deck_offset;
        }
    }
    public void GetCards()
    {
        StartCoroutine(WebRequest.instance.GetCards(Settings_Manager.instance.user_id));
    }
    public void GetDeck()
    {
        StartCoroutine(WebRequest.instance.GetDeck(deck_names.options[deck_names.value].text,Settings_Manager.instance.user_id,true));
        //StartCoroutine(WebRequest.instance.GetDeck("BestDeck", Settings_Manager.instance.user_id, true));
    }
    public void LoadDeck()
    {
        for(int i =0; i < deck_cards.Count; i++)
        {
            LoadCard(deck_cards[i]);
        }
        ArrangeDeck();
    }
    public void Clear()
    {
        foreach (GameObject card in deck)
        {
            Destroy(card);
        }
        deck.Clear();
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
    public void ImportExport()
    {
        ImportExportMenu.SetActive(!ImportExportMenu.activeSelf);
    }

    public void Export()
    {
        cardDict.Clear();
        foreach(int val in deck_cards)
        {
            if (!cardDict.ContainsKey(val))
            {
                cardDict.Add(val, 1001);
            }
            else {
                cardDict[val] += 1;
            }
        }
        exportField.text = "";
        foreach(KeyValuePair<int, int> entry in cardDict)
        {
            char c = (char)entry.Key;
            char r = (char)entry.Value;
            exportField.text += c + "" + r; 
        }
    }
    public void Copy()
    {
        GUIUtility.systemCopyBuffer = exportField.text;
    }
    public void Paste()
    {
        importField.text = GUIUtility.systemCopyBuffer;
    }

    public void Import()
    {
        deck_cards.Clear();
        for (int i =0; i < importField.text.Length; i += 2)
        {
            int j = importField.text[i];
            int k = importField.text[i + 1];
            k = k - 1000;
            for(int z = 0; z<k; z++)
            {
                deck_cards.Add(j);
            }
        }
        LoadDeck();
    }

}
