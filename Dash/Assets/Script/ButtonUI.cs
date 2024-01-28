using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public ButtonUI restartButton;

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
