using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject tutorialCanvas;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void TutorialButton()
    {
        if (tutorialCanvas != null)
        {
            tutorialCanvas.SetActive(true);
        }
    }
}

