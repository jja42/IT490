using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Manager : MonoBehaviour
{
    public static Settings_Manager instance;
    public bool historical;
    public string historical_date;
    public int user_id;
    public GameObject historical_ui;
    public InputField historical_input;
    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        user_id = 2;
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
}
