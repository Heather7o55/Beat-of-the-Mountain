using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadScene(string i)
    {
        SceneManager.LoadScene(i);
    }
}
