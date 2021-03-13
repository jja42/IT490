using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    public string str;
    string postURL;
    public string[] strarr;
    public List<int> intlist;
    public static WebRequest instance;
    private void Start()
    {
        instance = this;
        intlist = new List<int>();
    }
    #region API
    public IEnumerator Historical_Api_Request(string date)
    {
        postURL = "http://25.106.114.177/historical_api_request.php";
        WWWForm form = new WWWForm();
        form.AddField("date", date);
        UnityWebRequest www = UnityWebRequest.Post(postURL,form);
        yield return www.SendWebRequest();
        str = www.downloadHandler.text;
        HistoricalAPI_Parse(str);
    }
    public IEnumerator Api_Request()
    {
        postURL = "http://25.106.114.177/api_request.php";
        UnityWebRequest www = UnityWebRequest.Get(postURL);
        yield return www.SendWebRequest();
        str = www.downloadHandler.text;
        API_Parse(str);
    }
    public void API_Parse(string str)
    {
        strarr = str.Split('\n','>');

        Weather_Manager.instance.SetWeather(strarr[6], float.Parse(strarr[8]), int.Parse(strarr[10]), int.Parse(strarr[12]), float.Parse(strarr[14]));
    }
    public void HistoricalAPI_Parse(string str)
    {
        strarr = str.Split('\n', '>');

        Weather_Manager.instance.SetWeather(strarr[4], float.Parse(strarr[3]), int.Parse(strarr[5]), 10000, float.Parse(strarr[6]));
    }
    #endregion
    #region Deck
    public IEnumerator GetDeck(string deck_name,int user_id, bool edit)
    {
        postURL = "http://25.106.114.177/deck_get.php";
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
        print("L");
        Deck_Parse(str, edit);
    }
    public IEnumerator GetDeckList(int user_id, bool edit)
    {
        postURL = "http://25.106.114.177/deck_list_get.php";
        WWWForm form = new WWWForm();
        form.AddField("user", user_id);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        str = www.downloadHandler.text;
        Deck_List_Parse(str, edit);
    }
    public void Deck_Parse(string str, bool edit)
    {
        intlist.Clear();
        strarr = str.Split('\n', '>');
        for (int i = 10; i < 128; i+=2)
            intlist.Add(int.Parse(strarr[i]));
        if (edit)
        {
            Deck_Manager.instance.deck_cards = intlist;
            Deck_Manager.instance.LoadDeck();
        }
        else
        {
            Settings_Manager.instance.deck = intlist;
        }
    }
    public void Deck_List_Parse(string str, bool edit)
    {
        strarr = str.Split('\n', '>');
        if (edit)
        {
            Deck_Manager.instance.deck_name_list.Clear();
            for (int i = 9; i < strarr.Length - 2; i += 7)
                Deck_Manager.instance.deck_name_list.Add(strarr[i]);
            Deck_Manager.instance.SetDeckNames();
        }
        else
        {
            for (int i = 9; i < strarr.Length - 2; i += 7)
                Settings_Manager.instance.deck_name_list.Add(strarr[i]);
            Settings_Manager.instance.SetDeckNames();
        }
    }
    public IEnumerator SendDeck(List<int> deck, string deck_name,int user_id)
    {
        postURL = "http://25.106.114.177/deck_send.php";
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
        StartCoroutine(GetDeckList(Settings_Manager.instance.user_id, true));
    }
    public IEnumerator GetCards(int user_id)
    {
        postURL = "http://25.106.114.177/cards_get.php";
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
    public IEnumerator GetUserInfo(string username, string password)
    {
        postURL = "http://25.106.114.177/get_id.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        str = www.downloadHandler.text;
        UserInfoParse(str);
    }
    public void UserInfoParse(string str)
    {
        strarr = str.Split('\n', '>',' ');
        Settings_Manager.instance.user_id = int.Parse(strarr[12]);
    }
    public IEnumerator MatchHistory(int winner, int loser)
    {
        postURL = "http://25.106.114.177/match_history.php";
        WWWForm form = new WWWForm();
        form.AddField("winner", winner);
        form.AddField("loser", loser);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        str = www.downloadHandler.text;
    }

}
