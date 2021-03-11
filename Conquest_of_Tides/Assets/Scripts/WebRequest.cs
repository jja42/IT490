using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    string str;
    string postURL;
    public string[] strarr;
    public List<int> intlist;
    public static WebRequest instance;
    public enum RequestType
    {
        API,
        Card_Add,
        Cards_Get,
        Deck_Add,
        Deck_Get,
        Sign_In
    }
    private void Start()
    {
        instance = this;
        intlist = new List<int>();
    }
    #region API
    public IEnumerator Historical_Api_Request(string date)
    {
        postURL = "http://25.8.118.66/historical_api_request.php";
        WWWForm form = new WWWForm();
        form.AddField("date", date);
        UnityWebRequest www = UnityWebRequest.Post(postURL,form);
        yield return www.SendWebRequest();
        str = www.downloadHandler.text;
        HistoricalAPI_Parse(str);
    }
    public IEnumerator Api_Request()
    {
        postURL = "http://25.8.118.66/api_request.php";
        UnityWebRequest www = UnityWebRequest.Get(postURL);
        yield return www.SendWebRequest();
        str = www.downloadHandler.text;
        API_Parse(str);
    }
    public void API_Parse(string str)
    {
        strarr = str.Split('\n','>');

        Weather_Manager.instance.SetWeather(strarr[5], float.Parse(strarr[7]), int.Parse(strarr[9]), int.Parse(strarr[11]), float.Parse(strarr[13]));
    }
    public void HistoricalAPI_Parse(string str)
    {
        strarr = str.Split('\n', '>');

        Weather_Manager.instance.SetWeather(strarr[4], float.Parse(strarr[3]), int.Parse(strarr[5]), 10000, float.Parse(strarr[6]));
    }
    #endregion
    #region Deck
    public IEnumerator GetDeck(string deck_name,int user_id)
    {
        postURL = "http://25.8.118.66/deck_get.php";
        WWWForm form = new WWWForm();
        form.AddField("deck_name", deck_name);
        form.AddField("user", user_id);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        str = www.downloadHandler.text;
        Deck_Parse(str);
    }
    public IEnumerator GetDeckList(int user_id)
    {
        postURL = "http://25.8.118.66/deck_list_get.php";
        WWWForm form = new WWWForm();
        form.AddField("user", user_id);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        str = www.downloadHandler.text;
        Deck_List_Parse(str);
    }
    public void Deck_Parse(string str)
    {
        intlist.Clear();
        strarr = str.Split('\n', '>');
        for (int i = 9; i < 128; i+=2)
            intlist.Add(int.Parse(strarr[i]));
        Deck_Manager.instance.deck_cards = intlist;
        Deck_Manager.instance.LoadDeck();
    }
    public void Deck_List_Parse(string str)
    {
        strarr = str.Split('\n', '>');
        Deck_Manager.instance.deck_name_list.Clear();
        for (int i = 8; i < strarr.Length - 2; i += 7)
            Deck_Manager.instance.deck_name_list.Add(strarr[i]);
        Deck_Manager.instance.SetDeckNames();
    }
    public IEnumerator SendDeck(List<int> deck, string deck_name,int user_id)
    {
        postURL = "http://25.8.118.66/deck_send.php";
        WWWForm form = new WWWForm();
        form.AddField("user", user_id);
        form.AddField("deck_name", deck_name);
        for(int i = 1; i < 61; i++)
        {
            form.AddField("card_id_"+i.ToString(), deck[i-1].ToString());
        }
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        StartCoroutine(GetDeckList(Settings_Manager.instance.user_id));
    }
    public IEnumerator GetCards(int user_id)
    {
        postURL = "http://25.8.118.66/cards_get.php";
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_id);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        str = www.downloadHandler.text;
        //Cards_Parse(str);
    }
    public void Cards_Parse(string str)
    {
        intlist.Clear();
        strarr = str.Split('\n', '>');
        intlist.Add(int.Parse(strarr[0]));
        //Deck_Manager.instance.player_cards = intlist;
    }
    #endregion
}
