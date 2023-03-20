using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public event Action OnMoneyAdd;
    private const string PLAYER_PREFS_MONEY = "MoneyAmount";
    public static MoneyManager Instance;
    private int money;

    private void Awake()
    {
        Instance = this;
        money = PlayerPrefs.GetInt(PLAYER_PREFS_MONEY,0);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        PlayerPrefs.SetInt(PLAYER_PREFS_MONEY , money);
        PlayerPrefs.Save();
        OnMoneyAdd?.Invoke();
    }

    public int GetMoney()
    {
        return money;
    }

}
