using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
