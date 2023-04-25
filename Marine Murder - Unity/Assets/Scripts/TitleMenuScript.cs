using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuParent;
    [SerializeField] private GameObject creditsParent;

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }

    public void CreditsToggle()
    {
        menuParent.SetActive(!menuParent.activeSelf);
        creditsParent.SetActive(!creditsParent.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
