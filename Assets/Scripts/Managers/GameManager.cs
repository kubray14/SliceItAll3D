using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnLevelSuccess;
    public event Action OnPlayerDeath;
    public event Action OnPlayerRespawn;
    public event Action OnLevelStart;
    public event Action OnCollectKey;
    public int collectedKeyCount = 0;
    public static GameManager Instance;
    public PlayerController activePlayerController;
    public bool canPlayerDie = true;
    public bool isPlayerDead = false;
    public bool isGameStarted = false;
    public bool isGamePaused = false;
    public bool isLevelFinished = false;


    private void Awake()
    {
        Instance = this;
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

    public void ContinueLevel()
    {
        isPlayerDead = false;
        OnPlayerRespawn?.Invoke();
        StartCoroutine(DeathProtect_Coroutine());
        FindObjectOfType<PlayerController>().Jump();
    }

    public void PlayerDeath()
    {
        if (canPlayerDie)
        {
            isPlayerDead = true;
            canPlayerDie = false;
            OnPlayerDeath?.Invoke();
        }
    }
    private IEnumerator DeathProtect_Coroutine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        canPlayerDie = true;
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
        collectedKeyCount++;
        OnCollectKey?.Invoke();
    }

}
