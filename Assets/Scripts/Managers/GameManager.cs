using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameStarted = false;
    public bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        FindObjectOfType<PlayerController>().Jump();
        isGameStarted = true;
    }

    public void PauseGame()
    {
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
    }

}
