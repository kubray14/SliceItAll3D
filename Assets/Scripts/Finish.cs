using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour, IHittable
{
    [SerializeField] private TMP_Text moneyMultiplierText;
    public int moneyMultiplier = 1;

    private void Awake()
    {
        moneyMultiplierText = GetComponentInChildren<TMP_Text>();
        moneyMultiplierText.text = moneyMultiplier.ToString() + "X";
    }

    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        if (!GameManager.Instance.isLevelFinished)
        {
            if (isSharpEdgeCollided)
            {
                playerController.Stuck();
                FindObjectOfType<MoneyManager>().moneyMultiplier = moneyMultiplier;
                GameManager.Instance.FinishLevel(moneyMultiplier);

            }
            else
            {
                playerController.PushBack();
            }
        }
    }
}
