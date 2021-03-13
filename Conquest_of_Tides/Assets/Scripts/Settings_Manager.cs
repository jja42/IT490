using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Manager : MonoBehaviour
{
    public static Settings_Manager instance;
    public bool historical;
    public string historical_date;
    public string username;
    public string password;
    public bool demo;
    public int user_id;
    public string deck_name;
    public GameObject historical_ui;
    public InputField historical_input;
    public InputField username_input;
    public InputField password_input;
    public GameObject deck_ui;
    public GameObject sign_in_ui;
    public Dropdown deck_drop;
    public List<int> deck;
    public List<string> deck_name_list;
    public void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        deck = new List<int>();
    }
    public void SubmitHistorical()
    {
        historical_date = historical_input.text;
        historical = true;
    }
    public void EnableHistoricalUI()
    {
        historical_ui.SetActive(true);
    }
    public void Demo()
    {
        demo = true;
    }
    public void SubmitUserData()
    {
        username = username_input.text;
        password = password_input.text;
        sign_in_ui.SetActive(false);
        StartCoroutine(WebRequest.instance.GetUserInfo(username,password));
        EnableDeckUI();
    }
    public void EnableDeckUI()
    {
        deck_ui.SetActive(true);
    }
    public void SubmitDeck()
    {
        deck_name = deck_drop.options[deck_drop.value].text;
        StartCoroutine(WebRequest.instance.GetDeck(deck_name, user_id, false));
    }
    public void SetDeckNames()
    {
        deck_drop.ClearOptions();
        deck_drop.AddOptions(deck_name_list);
    }
}
