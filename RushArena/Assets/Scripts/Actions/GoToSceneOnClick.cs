using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToSceneOnClick : MonoBehaviour
{
    public Button MenuBtn;
    public Button QuitBtn;

    void Start()
    {
        MenuBtn.onClick.AddListener(GoHome);
        QuitBtn.onClick.AddListener(QuitGame);
    }

    void GoHome()
    {
        SceneManager.LoadScene("Loading");//to be able to reconnect to the server
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
