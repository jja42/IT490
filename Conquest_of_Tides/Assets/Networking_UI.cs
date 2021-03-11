using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Networking_UI : MonoBehaviour
{
    public GameObject Host_UI;
    public GameObject Join_UI;
    public GameObject Room_Join_UI;
    public GameObject Room_Host_UI;
    public GameObject Room_Menu_UI;
    public GameObject Error_UI;
    public GameObject Back_UI;
    public GameObject Exit_UI;
    public GameObject Connecting_UI;
    public static Networking_UI instance;
    public void Start()
    {
        instance = this;
    }
    public void DisableUI()
    {
        Host_UI.SetActive(false);
        Join_UI.SetActive(false);
        Room_Join_UI.SetActive(false);
        Room_Host_UI.SetActive(false);
        Room_Menu_UI.SetActive(false);
        Error_UI.SetActive(false);
        Back_UI.SetActive(false);
        Exit_UI.SetActive(false);
        Connecting_UI.SetActive(false);
    }
    public void Host()
    {
        if (Launcher.instance.can_interact)
        {
            DisableUI();
            Back_UI.SetActive(true);
            Room_Host_UI.SetActive(true);
        }
    }
    public void Join()
    {
        if (Launcher.instance.can_interact)
        {
            DisableUI();
            Back_UI.SetActive(true);
            Room_Join_UI.SetActive(true);
        }
    }
    public void Back()
    {
        DisableUI();
        Host_UI.SetActive(true);
        Join_UI.SetActive(true);
        Exit_UI.SetActive(true);
    }
    public void RoomMenu()
    {
        DisableUI();
        Room_Menu_UI.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
