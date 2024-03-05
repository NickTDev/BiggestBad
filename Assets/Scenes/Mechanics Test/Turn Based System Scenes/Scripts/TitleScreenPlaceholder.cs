// Merle Roji

using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenPlaceholder : MonoBehaviour
{
    [SerializeField] private string _sceneToSwitchTo;

    public void StartGame()
    {
        SceneManager.LoadScene(_sceneToSwitchTo);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
