using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool menuOpen = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenuPanel");
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (menuOpen == false) OpenMenu();
            else CloseMenu();
        }

    }

    public void OpenMenu()
    {
        if (menuOpen == false)
        {
            menuOpen = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else CloseMenu();
    }

    public void CloseMenu()
    {
        menuOpen = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
