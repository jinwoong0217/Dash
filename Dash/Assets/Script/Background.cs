using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void ReStartButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
