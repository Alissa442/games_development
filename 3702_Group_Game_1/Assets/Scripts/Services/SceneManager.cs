using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string sceneToLoad;
    public void LoadSceneDefinedScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    public void LoadMainMenu() => UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    public void LoadUpgradeMenu() => UnityEngine.SceneManagement.SceneManager.LoadScene("Upgrades");
    public void LoadSettingsMenu() => UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    public void LoadCreditsMenu() => UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    public void LoadScene1() => UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    public void LoadScene2() => UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2");
    public void LoadScene3() => UnityEngine.SceneManagement.SceneManager.LoadScene("Scene3");
    public void LoadScene4() => UnityEngine.SceneManagement.SceneManager.LoadScene("Scene4");
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
