using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnLevelSuccess;
    public event Action OnPlayerDeath;
    public event Action OnLevelStart;
    public event Action OnCollectKey;
    public static GameManager Instance;
    public PlayerController activePlayerController;
    private CameraController cameraController;
    public bool isPlayerDead = false;
    public bool isGameStarted = false;
    public bool isGamePaused = false;
    public bool isLevelFinished = false;


    private void Awake()
    {
        Instance = this;
        cameraController = FindObjectOfType<CameraController>();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        FindObjectOfType<TrailShowRoomUI>().SelectDefaultTrail();
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

    public void PlayerDeath()
    {
        isPlayerDead = true;
        OnPlayerDeath?.Invoke();
    }

    public void LevelFail()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void SetActiveKnife(PlayerController playerController)
    {
        activePlayerController = playerController;
    }

    public Transform GetActiveKnife()
    {
        return activePlayerController.transform;
    }

    public void CollectKey()
    {
        OnCollectKey?.Invoke();
    }

}
