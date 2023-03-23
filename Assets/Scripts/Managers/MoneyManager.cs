using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public event Action OnMoneyAdd;
    private const string PLAYER_PREFS_MONEY = "MoneyAmount";
    public static MoneyManager Instance;
    private int moneyTotal;
    private int money = 0;


    private void Awake()
    {
        Instance = this;
        moneyTotal = PlayerPrefs.GetInt(PLAYER_PREFS_MONEY, 0);
        money = 0;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyTotal += amount;
        OnMoneyAdd?.Invoke();
    }

    public void AddTotalMoney(int moneyMultiplier)
    {
        moneyTotal += money * moneyMultiplier;
        PlayerPrefs.SetInt(PLAYER_PREFS_MONEY, moneyTotal);
        PlayerPrefs.Save();
        OnMoneyAdd?.Invoke();
    }

    public int GetTotalMoney()
    {
        return moneyTotal;
    }

    public int GetMoney()
    {
        return money;
    }

}
