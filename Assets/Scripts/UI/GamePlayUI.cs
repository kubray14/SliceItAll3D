using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private TMP_Text moneyText;

    private void Awake()
    {
        optionsButton.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
            GameManager.Instance.PauseGame();
            optionsCanvas.SetActive(true);
        });
    }

    private void Start()
    {
        UpdateMoneyText();
        MoneyManager.Instance.OnMoneyAdd += MoneyManager_OnMoneyAdd;
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
