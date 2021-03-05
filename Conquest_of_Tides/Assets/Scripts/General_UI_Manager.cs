using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General_UI_Manager : MonoBehaviour
{
    public static General_UI_Manager instance;
    public GameObject Player_Hand;
    public GameObject Player_Active;
    public GameObject Player_Bench;
    List<GameObject> Player_Hand_Cards;
    List<GameObject> Player_Bench_Cards;
    List<GameObject> Card_Fortifications;
    //public GameObject Opponent_Hand;
    //List<GameObject> Opponent_Hand_Cards;
    public GameObject Attachment_UI;
    Vector3 Hand_left;
    Vector3 Hand_right;
    Vector3 Bench_left;
    Vector3 Bench_right;
    Vector3 Delta;
    void Start()
    {
        instance = this;
        Player_Hand_Cards = new List<GameObject>();
        Player_Bench_Cards = new List<GameObject>();
        Card_Fortifications = new List<GameObject>();
        Hand_left = Player_Hand.transform.position;
        Hand_right = Hand_left + new Vector3(13.5f, 0, 0);
        Bench_left = Player_Bench.transform.position + new Vector3(-4.5f, 0, 0);
        Bench_right = Bench_left + new Vector3(9f, 0, 0);
    }
    public void ArrangeHand(Card_Manager.Hand hand)
    {
        Player_Hand_Cards.Clear();
        for(int i = 0; i< Player_Hand.transform.childCount; i++)
        {
            Player_Hand_Cards.Add(Player_Hand.transform.GetChild(i).gameObject);
        }
        Delta = Hand_right - Hand_left;
        if(hand.cards.Count > 1)
            Delta /= hand.cards.Count - 1;
        for (int index = 0; index < hand.cards.Count; index++)
        {
            Player_Hand_Cards[index].transform.position = Hand_left + (Delta * index);
        }
    }
    public void ArrangeBench()
    {
        Player_Bench_Cards.Clear();
        for (int i = 0; i < Player_Bench.transform.childCount; i++)
        {
            Player_Bench_Cards.Add(Player_Bench.transform.GetChild(i).gameObject);
        }
        Delta = Bench_right - Bench_left;
        if (GameManager.instance.bench_count > 1)
            Delta /= GameManager.instance.bench_count - 1;
        for (int index = 0; index < GameManager.instance.bench_count; index++)
        {
            Player_Bench_Cards[index].transform.position = Bench_left + (Delta * index);
        }
    }
    public void MoveToActiveZone(GameObject obj)
    {
        obj.transform.position = Player_Active.transform.position;
    }
    public void MoveToBench(GameObject obj)
    {
        obj.transform.parent = Player_Bench.transform;
        ArrangeBench();
    }
    public void EnableAttachmentUI()
    {
        Attachment_UI.SetActive(true);
    }
    public void AttachFortification(GameObject Fortification, GameObject Ship)
    {
        Fortification.transform.parent = Ship.transform;
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
}
