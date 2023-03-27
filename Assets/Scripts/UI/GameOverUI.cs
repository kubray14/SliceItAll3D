using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private Button continueButton;
    [SerializeField] private Image continueImage;

    private void Start()
    {
        continueButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.RestartLevel();
            Hide();
        });

        GameManager.Instance.OnPlayerDeath += () => Show();
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject?.SetActive(false);
    }
}
