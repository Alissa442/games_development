using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenHandler : MonoBehaviour
{
    public float loadingTime = 5f;
    public float movingTime = 3f;

    public float originalYPos;
    public float targetYPos;

    public float currentYPos;

    public RectTransform titleImage;

    private float elapsedTime = 0f;
    private bool isMoving = true;

    void Start()
    {
        // Set the initial Y position of the title image
        currentYPos = originalYPos;
        SetYPosOfTitleImage(currentYPos);

        // At the loadingTime, load the main menu
        Invoke("LoadMainMenu", loadingTime);
    }

    void Update()
    {

        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / movingTime;

            // Slerp the Y position
            float newYPos = Mathf.Lerp(currentYPos, targetYPos, t);
            SetYPosOfTitleImage(newYPos);
            //transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);

            // Check if the slerp is complete
            if (elapsedTime >= movingTime)
            {
                isMoving = false;
                elapsedTime = 0f;
                currentYPos = targetYPos; // Update currentYPos to the final position
            }
        }

        // if a mouse button is pressed or a keyboard key is pressed, load the main menu
        if (Input.anyKeyDown)
        {
            LoadMainMenu();
        }
    }

    private void SetYPosOfTitleImage(float yPos)
    {
        // Set the Y position of the title image
        titleImage.anchoredPosition = new Vector2(titleImage.anchoredPosition.x, yPos);
    }

    private void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
