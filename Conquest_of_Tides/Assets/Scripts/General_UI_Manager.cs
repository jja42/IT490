using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class General_UI_Manager : MonoBehaviour
{
    public static General_UI_Manager instance;
    public GameObject Player_Hand;
    public GameObject Player_Active;
    public GameObject Player_Bench;
    List<GameObject> Player_Hand_Cards;
    List<GameObject> Player_Bench_Cards;
    public GameObject Opponent_Hand;
    public GameObject Opponent_Active;
    public GameObject Opponent_Bench;
    List<GameObject> Opponent_Hand_Cards;
    List<GameObject> Opponent_Bench_Cards;
    List<GameObject> Card_Fortifications;
    //public GameObject Opponent_Hand;
    //List<GameObject> Opponent_Hand_Cards;
    public GameObject Attachment_UI;
    public GameObject Reposition_UI;
    public GameObject Attack_UI;
    public Text Attack_Text;
    Vector3 Player_Hand_left;
    Vector3 Player_Hand_right;
    Vector3 Player_Bench_left;
    Vector3 Player_Bench_right;
    Vector3 Opponent_Hand_left;
    Vector3 Opponent_Hand_right;
    Vector3 Opponent_Bench_left;
    Vector3 Opponent_Bench_right;
    Vector3 Delta;
    public Text TurnPhase;
    public Text TurnPlayer;
    public GameObject Card_Prefab;
    private GameObject obj;
    void Start()
    {
        instance = this;
        Player_Hand_Cards = new List<GameObject>();
        Player_Bench_Cards = new List<GameObject>();
        Card_Fortifications = new List<GameObject>();
        Opponent_Bench_Cards = new List<GameObject>();
        Opponent_Hand_Cards = new List<GameObject>();
        Player_Hand_left = Player_Hand.transform.position;
        Player_Hand_right = Player_Hand_left + new Vector3(13.5f, 0, 0);
        Player_Bench_left = Player_Bench.transform.position + new Vector3(-4.5f, 0, 0);
        Player_Bench_right = Player_Bench_left + new Vector3(9f, 0, 0);
        Opponent_Hand_left = Opponent_Hand.transform.position;
        Opponent_Hand_right = Opponent_Hand_left - new Vector3(13.5f, 0, 0);
        Opponent_Bench_left = Opponent_Bench.transform.position - new Vector3(-4.5f, 0, 0);
        Opponent_Bench_right = Opponent_Bench_left - new Vector3(9f, 0, 0);
    }
    public void ArrangePlayerHand(Card_Manager.Hand hand)
    {
        Player_Hand_Cards.Clear();
        for(int i = 0; i< Player_Hand.transform.childCount; i++)
        {
            Player_Hand_Cards.Add(Player_Hand.transform.GetChild(i).gameObject);
        }
        Delta = Player_Hand_right - Player_Hand_left;
        if(hand.cards.Count > 1)
            Delta /= hand.cards.Count - 1;
        for (int index = 0; index < hand.cards.Count; index++)
        {
            Player_Hand_Cards[index].transform.position = Player_Hand_left + (Delta * index);
        }
    }
    public void ArrangeOpponentHand(Card_Manager.Hand hand)
    {
        Opponent_Hand_Cards.Clear();
        for (int i = 0; i < Opponent_Hand.transform.childCount; i++)
        {
            Opponent_Hand_Cards.Add(Opponent_Hand.transform.GetChild(i).gameObject);
        }
        Delta = Opponent_Hand_right - Opponent_Hand_left;
        if (hand.cards.Count > 1)
            Delta /= hand.cards.Count - 1;
        for (int index = 0; index < hand.cards.Count; index++)
        {
            Opponent_Hand_Cards[index].transform.position = Opponent_Hand_left + (Delta * index);
        }
    }
    public void ArrangePlayerBench()
    {
        Player_Bench_Cards.Clear();
        for (int i = 0; i < Player_Bench.transform.childCount; i++)
        {
            Player_Bench_Cards.Add(Player_Bench.transform.GetChild(i).gameObject);
        }
        Delta = Player_Bench_right - Player_Bench_left;
        if (GameManager.instance.player_bench_count > 1)
            Delta /= GameManager.instance.player_bench_count - 1;
        for (int index = 0; index < GameManager.instance.player_bench_count; index++)
        {
            Player_Bench_Cards[index].transform.position = Player_Bench_left + (Delta * index);
        }
    }
    public void ArrangeOpponentBench()
    {
        Opponent_Bench_Cards.Clear();
        for (int i = 0; i < Opponent_Bench.transform.childCount; i++)
        {
            Opponent_Bench_Cards.Add(Opponent_Bench.transform.GetChild(i).gameObject);
        }
        Delta = Opponent_Bench_right - Opponent_Bench_left;
        if (GameManager.instance.opponent_bench_count > 1)
            Delta /= GameManager.instance.opponent_bench_count - 1;
        for (int index = 0; index < GameManager.instance.opponent_bench_count; index++)
        {
            Opponent_Bench_Cards[index].transform.position = Opponent_Bench_left + (Delta * index);
        }
    }
    public void OpponentAttachFortificationActive(int fort_id)
    {
        obj = Opponent_Hand_Cards[0];
        Opponent_Hand_Cards.Remove(obj);
        Destroy(obj);
        obj = Instantiate(Card_Prefab, Opponent_Active.transform.GetChild(1).transform);
        obj.GetComponent<Player_Input>().card_id = fort_id;
        obj.GetComponent<Player_Input>().Reveal();
        if (Weather_Manager.instance.double_fortify)
        {
            obj = Instantiate(Card_Prefab, Opponent_Active.transform.GetChild(1).transform);
            obj.GetComponent<Player_Input>().card_id = fort_id;
            obj.GetComponent<Player_Input>().Reveal();
        }
        obj = Opponent_Active.transform.GetChild(1).gameObject;
        ArrangeFortifications(obj);
    }
    public void OpponentAttachFortificationBench(int fort_id,int index)
    {
        obj = Opponent_Hand_Cards[0];
        Opponent_Hand_Cards.Remove(obj);
        Destroy(obj);
        obj = Instantiate(Card_Prefab, Opponent_Bench.transform.GetChild(index).transform);
        obj.GetComponent<Player_Input>().card_id = fort_id;
        obj.GetComponent<Player_Input>().Reveal();
        if (Weather_Manager.instance.double_fortify)
        {
            obj = Instantiate(Card_Prefab, Opponent_Bench.transform.GetChild(index).transform);
            obj.GetComponent<Player_Input>().card_id = fort_id;
            obj.GetComponent<Player_Input>().Reveal();
        }
        obj = Opponent_Bench.transform.GetChild(index).gameObject;
        ArrangeFortifications(obj);
    }
    public void MoveToPlayerActiveZone(GameObject obj)
    {
        obj.transform.position = Player_Active.transform.position;
        obj.transform.parent = Player_Active.transform;
    }
    public void MoveToOpponentActiveZone(int id)
    {
        obj = Opponent_Hand_Cards[0];
        Opponent_Hand_Cards.Remove(obj);
        Destroy(obj);
        obj = Instantiate(Card_Prefab, Opponent_Active.transform);
        obj.GetComponent<Player_Input>().card_id = id;
        obj.GetComponent<Player_Input>().Reveal();
    }
    public void MoveToPlayerBench(GameObject obj)
    {
        obj.transform.parent = Player_Bench.transform;
        ArrangePlayerBench();
    }
    public void MoveToOpponentBench(int id)
    {
        obj = Opponent_Hand_Cards[0];
        Opponent_Hand_Cards.Remove(obj);
        Destroy(obj);
        obj = Instantiate(Card_Prefab, Opponent_Active.transform);
        obj.GetComponent<Player_Input>().card_id = id;
        obj.GetComponent<Player_Input>().Reveal();
        obj.transform.parent = Opponent_Bench.transform;
        ArrangeOpponentBench();
    }
    public void EnableAttachmentUI()
    {
        Attachment_UI.SetActive(true);
    }
    public void EnableRepositionUI()
    {
        Reposition_UI.SetActive(true);
    }
    public void EnableAttack_UI(Card_Manager.Card card)
    {
        Attack_UI.SetActive(true);
        Attack_Text.text = "Would you like to\n Attack with\n " + card.move_name_1 + "?";
    }
    public void DisableAttack_UI()
    {
        Attack_UI.SetActive(false);
    }
    public void AttachFortification(GameObject Fortification, GameObject Ship)
    {
        Fortification.transform.parent = Ship.transform;
        if(Weather_Manager.instance.double_fortify)
            Instantiate(Fortification, Ship.transform);
        ArrangeFortifications(Ship);
        Attachment_UI.SetActive(false);
    }
    public void ArrangeFortifications(GameObject Ship)
    {
        Card_Fortifications.Clear();
        for (int i = 0; i < Ship.transform.childCount; i++)
        {
            Card_Fortifications.Add(Ship.transform.GetChild(i).gameObject);
        }
        Delta = new Vector3(0.25f,0, 1);
        for (int index = 0; index < Card_Fortifications.Count; index++)
        {
            Card_Fortifications[index].transform.position = Ship.transform.position + (Delta * (index+1));
        }
    }
    public void PlayerReposition(GameObject ActiveShip, GameObject BenchShip)
    {
        MoveToPlayerActiveZone(BenchShip);
        BenchShip.GetComponent<Player_Input>().bench = false;
        BenchShip.GetComponent<Player_Input>().active = true;
        MoveToPlayerBench(ActiveShip);
        ActiveShip.GetComponent<Player_Input>().bench = true;
        ActiveShip.GetComponent<Player_Input>().active = false;
        Reposition_UI.SetActive(false);
    }
    public void OpponentReposition(int index)
    {
        GameObject BenchShip = Opponent_Bench.transform.GetChild(index).gameObject;
        BenchShip.transform.position = Opponent_Active.transform.position;
        BenchShip.transform.parent = Opponent_Active.transform;
        BenchShip.GetComponent<Player_Input>().bench = false;
        BenchShip.GetComponent<Player_Input>().active = true;
        GameObject ActiveShip = Opponent_Active.transform.GetChild(1).gameObject;
        ActiveShip.transform.parent = Opponent_Bench.transform;
        ActiveShip.GetComponent<Player_Input>().bench = true;
        ActiveShip.GetComponent<Player_Input>().active = false;
        ArrangeOpponentBench();
    }
    public void SetPhase()
    {
        TurnPhase.text = Turn_Manager.instance.currState.ToString() + " Phase";
    }
    public void SetPlayer()
    {
        if(PlayerTurnManager.instance.turn_id == (int)Turn_Manager.instance.currPlayer)
        {
            TurnPlayer.text = "Your Turn";
        }
        else
        {
            TurnPlayer.text = "Opponent's Turn";
        }
    }
}
