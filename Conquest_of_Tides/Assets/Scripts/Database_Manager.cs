using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Manager : MonoBehaviour
{
    public static Database_Manager instance;
    public List<Card_Manager.Card> Database;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Database = new List<Card_Manager.Card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateDatabase()
    {
        Database = new List<Card_Manager.Card>
        {
            new Card_Manager.Card(1001,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1002,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1003,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1004,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1005,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1006,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1007,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1008,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1009,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1010,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1011,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1012,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1013,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1014,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO"),
            new Card_Manager.Card(1015,"test",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","move 1 description",40,"1100","move 2","move 2 description",20,"3000","5000","flavor text LMAO")
        };
    }
    public Card_Manager.Card GetCard(int card_id)
    {
        return Database.Find(card => card.card_id == card_id);
    }
}
