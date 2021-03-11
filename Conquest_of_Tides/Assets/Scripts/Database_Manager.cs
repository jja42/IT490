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
            new Card_Manager.Card(1001,"Ironclad_1",Card_Manager.CardType.Ship,Card_Manager.Type.Ironclad,40,"test_ability","test description for ability","move 1","Move 1 description",20,"1-1-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1002,"Ironclad_2",Card_Manager.CardType.Ship,Card_Manager.Type.Ironclad,60,"test_ability","test description for ability","move 1","Move 1 description",30,"1-7-7-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1003,"Ironclad_3",Card_Manager.CardType.Ship,Card_Manager.Type.Ironclad,40,"test_ability","test description for ability","Move 1","move 1 description",10,"1-0-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1004,"Ironclad_4",Card_Manager.CardType.Ship,Card_Manager.Type.Ironclad,80,"test_ability","test description for ability","Move 1","move 1 description",20,"1-1-0-0","move 2","move 2 description",20,"3000","7-7-0-0","flavor text LMAO"),
            new Card_Manager.Card(1005,"Ironclad_5",Card_Manager.CardType.Ship,Card_Manager.Type.Ironclad,80,"test_ability","test description for ability","Move 1","move 1 description",40,"1-1-1-0","move 2","move 2 description",20,"3000","0-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1006,"Galleon_1",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,40,"test_ability","test description for ability","Move 1","move 1 description",20,"3-0-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1007,"Galleon_2",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,60,"test_ability","test description for ability","Move 1","move 1 description",20,"3-7-7-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1008,"Galleon_3",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,30,"test_ability","test description for ability","Move 1","move 1 description",10,"7-0-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1009,"Galleon_4",Card_Manager.CardType.Ship,Card_Manager.Type.Galleon,100,"test_ability","test description for ability","move 1","Move 1 description",40,"3-3-3-3","move 2","move 2 description",20,"3000","7-7-7-7","flavor text LMAO"),
            new Card_Manager.Card(1010,"Submarine_1",Card_Manager.CardType.Ship,Card_Manager.Type.Submarine,30,"test_ability","test description for ability","move 1","Move 1 description",30,"6-6-0-0","move 2","move 2 description",20,"3000","0-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1011,"Submarine_2",Card_Manager.CardType.Ship,Card_Manager.Type.Submarine,50,"test_ability","test description for ability","move 1","Move 1 description",20,"6-0-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1012,"Submarine_3",Card_Manager.CardType.Ship,Card_Manager.Type.Submarine,80,"test_ability","test description for ability","move 1","Move 1 description",40,"6-6-7-0","move 2","move 2 description",20,"3000","7-7-7-0","flavor text LMAO"),
            new Card_Manager.Card(1013,"Submarine_4",Card_Manager.CardType.Ship,Card_Manager.Type.Submarine,100,"test_ability","test description for ability","move 1","Move 1 description",60,"6-6-6-7","move 2","move 2 description",20,"3000","7-7-7-0","flavor text LMAO"),
            new Card_Manager.Card(1014,"Battleship_1",Card_Manager.CardType.Ship,Card_Manager.Type.Battleship,40,"test_ability","test description for ability","move 1","Move 1 description",20,"7-7-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1015,"Battleship_2",Card_Manager.CardType.Ship,Card_Manager.Type.Battleship,50,"test_ability","test description for ability","move 1","Move 1 description",30,"2-7-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1016,"Battleship_3",Card_Manager.CardType.Ship,Card_Manager.Type.Battleship,80,"test_ability","test description for ability","move 1","Move 1 description",50,"2-2-7-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(1017,"Battleship_4",Card_Manager.CardType.Ship,Card_Manager.Type.Battleship,60,"test_ability","test description for ability","move 1","Move 1 description",20,"2-7-0-0","move 2","move 2 description",20,"3000","7-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(2001,"Ironclad_Fortification",Card_Manager.CardType.Fortification,Card_Manager.Type.Ironclad,80,"test_ability","test description for ability","move 1","Move 1 description",40,"1100","move 2","move 2 description",20,"3000","0-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(2002,"Battleship_Fortification",Card_Manager.CardType.Fortification,Card_Manager.Type.Battleship,80,"test_ability","test description for ability","move 1","Move 1 description",40,"1100","move 2","move 2 description",20,"3000","0-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(2003,"Galleon_Fortification",Card_Manager.CardType.Fortification,Card_Manager.Type.Galleon,80,"test_ability","test description for ability","move 1","Move 1 description",40,"1100","move 2","move 2 description",20,"3000","0-0-0-0","flavor text LMAO"),
            new Card_Manager.Card(2006,"Submarine_Fortification",Card_Manager.CardType.Fortification,Card_Manager.Type.Submarine,80,"test_ability","test description for ability","move 1","Move 1 description",40,"1100","move 2","move 2 description",20,"3000","0-0-0-0","flavor text LMAO")
        };
    }
    public Card_Manager.Card GetCard(int card_id)
    {
        return Database.Find(card => card.card_id == card_id);
    }
    public void UpdateDatabase()
    {
        Database.ForEach(card => card.move_damage_1 += 20);
    }
}
