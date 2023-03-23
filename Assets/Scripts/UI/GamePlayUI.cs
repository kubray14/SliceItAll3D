using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button restartButton;


    private void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.RestartLevel();
        });

        UpdateMoneyText();
        MoneyManager.Instance.OnMoneyAdd += MoneyManager_OnMoneyAdd;

        restartButton.gameObject.SetActive(false);
        GameManager.Instance.OnLevelStart += () => restartButton.gameObject.SetActive(true);
    }

    private void MoneyManager_OnMoneyAdd()
    {
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = MoneyManager.Instance.GetTotalMoney() + "$";
    }
}
