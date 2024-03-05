using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainOrQuitUI : MonoBehaviour
{
    [SerializeField] private string _gameScene;

    public void OnQuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(_gameScene);
    }
}
