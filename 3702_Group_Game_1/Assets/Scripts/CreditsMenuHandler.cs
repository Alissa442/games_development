using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenuHandler : MonoBehaviour
{

    void Update()
    {
        // if a mouse button is pressed or a keyboard key is pressed, load the main menu
        if (Input.anyKeyDown)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

    }
}
