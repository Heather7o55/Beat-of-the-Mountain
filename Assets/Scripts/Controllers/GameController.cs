using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public GameObject winMenu;
    public GameObject pauseMenu;
    public RhythmManager rhythmManager;
    void Update()
    {
        if(Input.GetKeyDown("escape")) PauseGame();
        if(rhythmManager.currentBeat >= rhythmManager.activeSong.totalBeats) GameWin();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadScene(string i)
    {
        SceneManager.LoadScene(i);
    }
    public void PauseGame()
    {
        rhythmManager.PauseSong();
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        rhythmManager.StartSong();
    }
    public void GameWin()
    {
        text.text = $"Score:{rhythmManager.score}";
        winMenu.SetActive(true);
    }
}
