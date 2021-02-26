using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_UI_Manager : MonoBehaviour
{
    #region Card_Popup_Vars
    //GameObject to enable/disable for popup
    public GameObject Card_Popup;
    //Image for the center of the card
    public Image Card_Image;
    //Image for the card backing
    public Image Card_Back;
    //Name of the card
    public Text Card_Name;
    //HP for ships
    public Text Card_HP;
    //Gameobject to toggle inactive if card has no ability
    public GameObject Ability;
    //Name of ability
    public Text Ability_Name;
    //Ability description
    public Text Ability_Text;
    //Gameobject to toggle inactive if there is no move 1
    public GameObject Move_1;
    //Name of move 1
    public Text Move_Name_1;
    //Description of move 1 or general card description
    public Text Move_Text_1;
    //Damage dealt by move 1
    public Text Move_Damage_1;
    //Gameobject to toggle inactive if move 1 has no cost
    public GameObject Move_1_Cost;
    //Fortifications needed for move 1, can be none or up to 4 fortifications
    public Image Move_1_Cost_1;
    public Image Move_1_Cost_2;
    public Image Move_1_Cost_3;
    public Image Move_1_Cost_4;
    //Gameobject to toggle inactive if there is no Move 2
    public GameObject Move_2;
    //Name of move 2
    public Text Move_Name_2;
    //Description of move 2
    public Text Move_Text_2;
    //Damage dealt by move 2
    public Text Move_Damage_2;
    //Gameobject to toggle inactive if move 2 has no cost
    public GameObject Move_2_Cost;
    //Fortifications needed for move 1, can be none or up to 4 fortifications
    public Image Move_2_Cost_1;
    public Image Move_2_Cost_2;
    public Image Move_2_Cost_3;
    public Image Move_2_Cost_4;
    //Gameobject to toggle inactive if there is no Weakness
    public GameObject Weakness;
    //Icon for the weakness type
    public Image Weakness_Img;
    //Weakness value
    public Text Weakness_Text;
    //Gameobject to toggle inactive if there is no resistance
    public GameObject Resistance;
    //Icon for the resistance type
    public Image Resistance_Img;
    //Resistance value
    public Text Resistance_Text;
    //Gameobject to toggle inactive if there is no Reposition Cost
    public GameObject Reposition_Cost;
    //Fortifications needed to reposition
    public Image Reposition_Cost_1;
    public Image Reposition_Cost_2;
    public Image Reposition_Cost_3;
    public Image Reposition_Cost_4;
    //Flavorful description or lore
    public Text Flavor_Text;
    #endregion

    private string path;
    public static Card_UI_Manager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public void PopupCard(int card_id)
    {
        Card_Manager.Card card = Card_Manager.instance.GetCardByID(card_id); 
        //make gameobject active
        Card_Popup.SetActive(true);
        //Use appropriate function based on card type
        switch (card.card_type)
        {
            case Card_Manager.CardType.Ship:
                Setup_Ship(card);
                break;
            case Card_Manager.CardType.Reinforcement:
                Setup_Reinforcement(card);
                break;
            case Card_Manager.CardType.Fortification:
                Setup_Fortification(card);
                break;
        }
        
    }
    public void RemovePopup()
    {
        Card_Popup.SetActive(false);
    }
    public void Setup_Ship(Card_Manager.Card card)
    {
        //set card image
        path = "temp_assets/" + card.id.ToString();
        Card_Image.sprite = Resources.Load<Sprite>(path);
        Card_Image.rectTransform.anchoredPosition = new Vector3(0.2405f, 77.64047f, 0f);
        //set card back
        path = "temp_assets/Ship_type_" + (int)card.type;
        Card_Back.sprite = Resources.Load<Sprite>(path);
        //set card name
        Card_Name.text = card.name;
        Card_Name.rectTransform.anchoredPosition = new Vector3(-9, 170.1299f, 0);
        //set card hp
        Card_HP.text = card.hp.ToString() + "HP";
        //set card ability
        if(card.ability_name == "None")
        {
            Ability.SetActive(false);
        }
        else
        {
            Ability.SetActive(true);
            Ability_Name.text = card.ability_name;
            Ability_Text.text = card.ability_description;
        }
        //set move 1
        Move_Name_1.text = card.move_name_1;
        Move_Text_1.text = card.move_description_1;
        Move_Text_1.rectTransform.anchoredPosition = new Vector3(-5.3f, -63.32768f, 0);
        Move_Text_1.rectTransform.sizeDelta = new Vector2(229, 23.75f);
        Move_Damage_1.text = card.move_damage_1.ToString();
        path = "temp_assets/Fortification_type_" + card.move_cost_1[0].ToString();
        Move_1_Cost_1.sprite = Resources.Load<Sprite>(path);
        path = "temp_assets/Fortification_type_" + card.move_cost_1[1].ToString();
        Move_1_Cost_2.sprite = Resources.Load<Sprite>(path);
        path = "temp_assets/Fortification_type_" + card.move_cost_1[2].ToString();
        Move_1_Cost_3.sprite = Resources.Load<Sprite>(path);
        path = "temp_assets/Fortification_type_" + card.move_cost_1[3].ToString();
        Move_1_Cost_4.sprite = Resources.Load<Sprite>(path);
        //set move 2
        if (card.move_name_2 == "None")
        {
            Move_2.SetActive(false);
        }
        else
        {
            Move_2.SetActive(true);
            Move_Name_2.text = card.move_name_2;
            Move_Text_2.text = card.move_description_2;
            Move_Damage_2.text = card.move_damage_2.ToString();
            path = "temp_assets/Fortification_type_" + card.move_cost_2[0].ToString();
            Move_2_Cost_1.sprite = Resources.Load<Sprite>(path);
            path = "temp_assets/Fortification_type_" + card.move_cost_2[1].ToString();
            Move_2_Cost_2.sprite = Resources.Load<Sprite>(path);
            path = "temp_assets/Fortification_type_" + card.move_cost_2[2].ToString();
            Move_2_Cost_3.sprite = Resources.Load<Sprite>(path);
            path = "temp_assets/Fortification_type_" + card.move_cost_2[3].ToString();
            Move_2_Cost_4.sprite = Resources.Load<Sprite>(path);
        }
        //set weakness
        path = "temp_assets/Fortification_type_" + (int)Card_Manager.instance.Get_Weakness(card.type);
        Weakness_Img.sprite = Resources.Load<Sprite>(path);
        Weakness_Text.text = "+20";
        //set resistance
        path = "temp_assets/Fortification_type_" + (int)Card_Manager.instance.Get_Resistance(card.type);
        if (Card_Manager.instance.Get_Resistance(card.type) == Card_Manager.Type.None)
            Resistance.SetActive(false);
        else
        {
            Resistance.SetActive(true);
            Resistance_Img.sprite = Resources.Load<Sprite>(path);
            Resistance_Text.text = "-20";
        }
        //set reposition cost
        if (card.reposition_cost == "None")
        {
            Reposition_Cost.SetActive(false);
        }
        else
        {
            Reposition_Cost.SetActive(true);
            path = "temp_assets/Fortification_type_" + card.reposition_cost[0].ToString();
            Reposition_Cost_1.sprite = Resources.Load<Sprite>(path);
            path = "temp_assets/Fortification_type_" + card.reposition_cost[1].ToString();
            Reposition_Cost_2.sprite = Resources.Load<Sprite>(path);
            path = "temp_assets/Fortification_type_" + card.reposition_cost[2].ToString();
            Reposition_Cost_3.sprite = Resources.Load<Sprite>(path);
            path = "temp_assets/Fortification_type_" + card.reposition_cost[3].ToString();
            Reposition_Cost_4.sprite = Resources.Load<Sprite>(path);
        }
        //set flavor text
        Flavor_Text.text = card.flavor_text;
    }
    public void Setup_Reinforcement(Card_Manager.Card card)
    {
        //set card image
        path = "temp_assets/" + card.id.ToString();
        Card_Image.sprite = Resources.Load<Sprite>(path);
        Card_Image.rectTransform.anchoredPosition = new Vector3(0.2405f, 65,0f);
        //set card back
        path = "temp_assets/Reinforcement_Card";
        Card_Back.sprite = Resources.Load<Sprite>(path);
        //remove card name
        Card_Name.text = card.name;
        Card_Name.rectTransform.anchoredPosition = new Vector3(18, 145.29f, 0);
        //remove card hp
        Card_HP.text = "";
        //disable card ability
        Ability.SetActive(false);
        //remove move 1 name
        Move_Name_1.text = "";
        //set move 1 description
        Move_Text_1.text = card.move_description_1;
        Move_Text_1.rectTransform.anchoredPosition = new Vector3(5f, -180, 0);
        Move_Text_1.rectTransform.sizeDelta = new Vector2(227, 300);
        //remove move 1 damage
        Move_Damage_1.text = "";
        //disable move 1 cost
        Move_1_Cost.SetActive(false);
        //disable move 2
        Move_2.SetActive(false);
        //disable weakness
        Weakness.SetActive(false);
        //disable resistance
        Resistance.SetActive(false);
        //disable reposition cost
        Reposition_Cost.SetActive(false);
        //set flavor text
        Flavor_Text.text = "";
    }
    public void Setup_Fortification(Card_Manager.Card card)
    {
        //set card image
        path = "temp_assets/blank";
        Card_Image.sprite = Resources.Load<Sprite>(path);
        //set card back
        path = "temp_assets/Fortification_card_" + (int)card.type;
        Card_Back.sprite = Resources.Load<Sprite>(path);
        //remove card name
        Card_Name.text = "";
        //remove card hp
        Card_HP.text = "";
        //disable card ability
        Ability.SetActive(false);
        //disable move 1
        Move_1.SetActive(false);
        //disable move 2
        Move_2.SetActive(false);
        //disable weakness
        Weakness.SetActive(false);
        //disable resistance
        Resistance.SetActive(false);
        //disable reposition cost
        Reposition_Cost.SetActive(false);
        //remove flavor text
        Flavor_Text.text = "";
    }
}
