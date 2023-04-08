using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    private GameObject lastSelected;
    private GameObject currentlySelected;

    public GameObject leaderboard, credits;
    public GameObject leaderboardButton, creditsButton;

    public GameObject startButton;

    bool gamepad = false;

    bool navButtonPressed = false;
    public void StartGane()
    {
        SceneManager.LoadScene("Stage 1");
    }


    //Dpad navigation to different menus
    public void Leaderboard()
    {
        if (EventSystem.current.currentSelectedGameObject != leaderboardButton)
        {
            EventSystem.current.SetSelectedGameObject(leaderboardButton);
        }
        else
            EventSystem.current.SetSelectedGameObject(leaderboard);
    }

    public void Credits()
    {
        if (EventSystem.current.currentSelectedGameObject != creditsButton)
        {
            EventSystem.current.SetSelectedGameObject(creditsButton);
        }
        else
            EventSystem.current.SetSelectedGameObject(credits);
    }

    public void Start()
    {
        //see if we are using a gamepad or keyboard & mouse
        var input = FindObjectOfType<PlayerInput>();

        if (input.currentControlScheme == "Gamepad")
        {
            gamepad = true;
        }

        else gamepad = false;
    }

    private void Update()
    {
        if (currentlySelected == null && gamepad == true)
        {
            currentlySelected = EventSystem.current.currentSelectedGameObject;
            lastSelected = currentlySelected;
        }

        //using mouse&keyboard so highlight nothing unless pressing arrowkeys/wasd
        else if (gamepad == false && navButtonPressed == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ResetSelected()
    {
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        //pressed dpad/arrowkeys etc so highlights are activated
        if (navButtonPressed == false)
        {
            EventSystem.current.SetSelectedGameObject(startButton);
            currentlySelected = EventSystem.current.currentSelectedGameObject;
            lastSelected = currentlySelected;
            navButtonPressed = true;
        }

        //menu navigation with dpad or wasd/arrows works even if user clicks away
        if (context.performed)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                currentlySelected = lastSelected;
                EventSystem.current.SetSelectedGameObject(lastSelected);

            }
            else
            {
                currentlySelected = EventSystem.current.currentSelectedGameObject;
                lastSelected = currentlySelected;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
