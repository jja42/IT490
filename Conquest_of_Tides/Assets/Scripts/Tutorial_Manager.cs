using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Manager : MonoBehaviour
{
    int Tutorial_Stage;
    public Text Tutorial_Text;
    public Text button;
    public GameObject Tutorial_obj;
    enum Stage:int
    {
        Welcome = 0,
        Card_Types = 1,
        Ships = 2,
        Ship_Details = 3,
        Fortifications = 4,
        Active = 5,
        Bench = 6,
        Repositioning = 7,
        Attaching = 8,
        Turn_Phases = 9,
        Turns = 10,
        Combat = 11,
        Weather = 12,
        Weather_Cont = 13,
        End = 14
    }
    // Start is called before the first frame update
    void Start()
    {
        Tutorial_Stage = 0;
    }

    public void Tutorial()
    {
        Tutorial_obj.SetActive(!Tutorial_obj.activeSelf);
    }

    // Update is called once per frame
    public void Next()
    {
        Tutorial_Stage++;
        if(Tutorial_Stage > 14)
        {
            Tutorial_Stage = 0;
        }
        ChangeStage(Tutorial_Stage);
    }

    void ChangeStage(int tutorial_stage)
    {
        Stage stage = (Stage)tutorial_stage;
        switch (stage)
        {
            case Stage.Welcome:
                Tutorial_Text.text = "Welcome\n\nThis Tutorial will teach You\n\nthe Basics of\n\nHow to Play this Game\n";
                button.text = "Next";
                break;
            case Stage.Card_Types:
                Tutorial_Text.text = "Card Types\n\nThere are Two\nTypes of Cards\n\nShip Cards\nand\nFortification Cards\n";
                break;
            case Stage.Ships:
                Tutorial_Text.text = "Ship Cards\n\nShip Cards make up\nyour Fleet\n\n You will be using them to\n Attack your Opponent\n";
                break;
            case Stage.Ship_Details:
                Tutorial_Text.text = "Ship Details\n\nYou can Hover over your\nShips to get more\nDetails about them\n\nUse this to Inform\nyour Choices\n";
                break;
            case Stage.Fortifications:
                Tutorial_Text.text = "Fortifications\n\nFortifications are what\ngive your Ships Strength\n\nYou need them to\nPower each Ship's Attack\n";
                break;
            case Stage.Active:
                Tutorial_Text.text = "Active Zone\n\nYou can place One\nShip into the Active Zone\n\nThis will allow it to attack\nyour Opponent's Ships\n";
                break;
            case Stage.Bench:
                Tutorial_Text.text = "Bench Zone\n\nYou can place your other\nShips into the Bench Zone\n\nThis allows you to\nReposition them later\n";
                break;
            case Stage.Repositioning:
                Tutorial_Text.text = "Repositioning\n\nOnce per Turn you can\nReposition your Ships\n\nA Benched Ship can swap\nplaces with an Active Ship\n";
                break;
            case Stage.Attaching:
                Tutorial_Text.text = "Attaching\n\nOnce per Turn you can\nalso Attach a Fortification\n\nFortifications can be attached to either\nActive or Benched Ships\n";
                break;
            case Stage.Turn_Phases:
                Tutorial_Text.text = "Turn Phases\n\nTurns are broken up into\nFour Phases\nDraw Main Combat End\n\nClicking on the Deck\nCycles through the Phases\n";
                break;
            case Stage.Turns:
                Tutorial_Text.text = "Turns\n\nDraw Phase will draw a card\nMain Phase is for most actions\nCombat phase allows you to attack\n\nOnce you have concluded your Turn\nEnd Phase will transition to your\nopponent and their Turn\n";
                break;
            case Stage.Combat:
                Tutorial_Text.text = "Combat\n\nDuring the Combat Phase\nYou can Click on your Ship\nto Engage in Combat\n\nIf you have enough Power\nyour Ship will Attack the\nopponent's Ship\n";
                break;
            case Stage.Weather:
                Tutorial_Text.text = "Weather\n\nIn addition to Battling\nyour Opponent you must\nalso bare the Weather\n\nWeather can have a variety of effects on your sailing\n";
                break;
            case Stage.Weather_Cont:
                Tutorial_Text.text = "Weather UI\n\nTo see what sort of Weather\nEffects are in play\n\nSimply Click on the Symbol\nthat Represents the Weather\n";
                break;
            case Stage.End:
                Tutorial_Text.text = "The End\n\nThat concludes the Tutorial\n\nFeel Free to Test out any of\nthe Features mentioned Prior\n\nOr Click Back to Repeat\nthis Tutorial\n";
                button.text = "Back";
                break;
        }
    }
}
