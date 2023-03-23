using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWinUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private void Start()
    {
        continueButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.NextLevel();
        });

        GameManager.Instance.OnLevelSuccess += GameManager_OnLevelFinish;

        Hide();
    }

    private void GameManager_OnLevelFinish()
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
