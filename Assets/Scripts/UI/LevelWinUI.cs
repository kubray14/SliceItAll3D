using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWinUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        continueButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.NextLevel();
        });

        GameManager.Instance.OnLevelSuccess += GameManager_OnLevelSuccess;

        Hide();
    }

    private void GameManager_OnLevelSuccess()
    {
        Show(GetEarnedTotalMoney());
    }

    private void Show(string earnedTotalMoney)
    {
        gameObject.SetActive(true);
        moneyText.text = earnedTotalMoney;
    }

    private string GetEarnedTotalMoney()
    {
        return MoneyManager.Instance.GetEarnedTotalMoney().ToString();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
