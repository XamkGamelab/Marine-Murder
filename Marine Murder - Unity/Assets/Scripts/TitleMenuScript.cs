using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuParent;
    [SerializeField] private GameObject creditsParent;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        audioSource.Play();
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }

    public void CreditsToggle()
    {
        audioSource.Play();
        menuParent.SetActive(!menuParent.activeSelf);
        creditsParent.SetActive(!creditsParent.activeSelf);
    }

    public void QuitGame()
    {
        audioSource.Play();
        Application.Quit();
    }
}
