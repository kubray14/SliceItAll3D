using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameStarted = false;
    public bool isGamePaused = false;
    public bool isLevelFinished = false;

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

    public void FinishLevel(int moneyMultiplier)
    {
        if (!isLevelFinished)
        {
            isLevelFinished = true;
            FindObjectOfType<MoneyManager>().AddTotalMoney(moneyMultiplier);
            Time.timeScale = 0;
        }
    }

}
