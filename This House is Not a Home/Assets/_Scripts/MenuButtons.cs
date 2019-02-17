using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    // class that handles all the menu fuctions

    public GameObject startMenu;
    public GameObject instructions;
    // Use this for initialization
    void Start()
    {
        startMenu.SetActive(true);
        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playGame()
    {
        startMenu.SetActive(false);
        instructions.SetActive(false);
        GameManager.instance.CreateTask();
    }

    public void insturctionsMenu()
    {
        startMenu.SetActive(false);
        instructions.SetActive(true);
    }

    public void returnMenu()
    {
        startMenu.SetActive(true);
        instructions.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
