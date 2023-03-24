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
            GameManager.Instance.ContinueLevel();
            Hide();
        });

        GameManager.Instance.OnPlayerDeath += () => Show();
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartCountdown_Coroutine());
    }

    private void Hide()
    {
        gameObject?.SetActive(false);
    }

    private IEnumerator StartCountdown_Coroutine()
    {
        float countDownTimerMax = 3.0f;
        float countDownTimer = countDownTimerMax;
        while (true)
        {
            yield return null;
            countDownTimer -= Time.deltaTime;

            if (countDownTimer <= 0)
            {
                GameManager.Instance.LevelFail();
            }
            continueImage.fillAmount = countDownTimer / countDownTimerMax;

        }
    }
}
