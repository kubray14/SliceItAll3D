using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnLevelSuccess;
    public event Action OnPlayerDeath;
    public event Action OnLevelStart;
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
        OnLevelStart?.Invoke();
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
            OnLevelSuccess?.Invoke();
        }
    }

    public void ContinueLevel()
    {
        print("Level Continued");
    }

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public void LevelFail()
    {
        LevelManager.Instance.RestartLevel();
    }

}
