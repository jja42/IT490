using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General_UI_Manager : MonoBehaviour
{
    public static General_UI_Manager instance;
    public GameObject Player_Hand;
    List<GameObject> Player_Hand_Cards;
    public GameObject Opponent_Hand;
    List<GameObject> Opponent_Hand_Cards;
    Vector3 Hand_left;
    Vector3 Hand_right;
    Vector3 Delta;
    void Start()
    {
        instance = this;
    }
    public void ArrangeHand(Card_Manager.Hand hand)
    {
        Player_Hand_Cards.Clear();
        for(int i = 0; i< Player_Hand.transform.childCount; i++)
        {
            Player_Hand_Cards.Add(Player_Hand.transform.GetChild(i).gameObject);
        }
        Delta = Hand_left - Hand_right;
        Delta /= hand.cards.Count - 1;
        for (int index = 0; index < hand.cards.Count; index++)
        {
            Player_Hand_Cards[index].transform.position = Hand_left + (Delta * index);
        }
    }
}
