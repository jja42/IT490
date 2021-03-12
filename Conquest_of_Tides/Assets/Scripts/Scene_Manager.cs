using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void MainScene()
    {
        SceneManager.LoadScene(2);
    }
    public void DeckEdit()
    {
        SceneManager.LoadScene(3);
    }
    public void Demo()
    {
        SceneManager.LoadScene(4);
    }
    public void ExitScene()
    {
        Application.Quit();
    }
}
