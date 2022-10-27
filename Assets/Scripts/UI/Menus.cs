using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("2DTopDown");
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("Menus");
    }
    public void GoLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void GoCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
