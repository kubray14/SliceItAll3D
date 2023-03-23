using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeShowRoom : MonoBehaviour
{
    private const string PLAYER_PREFS_KNIFE_NUMBER = "KnifeNumber";
    [SerializeField] private List<Button> knifeButtons;
    private KnifeSelecter knifeSelecter;

    private int knifeNumber;

    private void Awake()
    {
        knifeSelecter = FindObjectOfType<KnifeSelecter>();
    }

    private void Start()
    {
        knifeNumber = PlayerPrefs.GetInt(PLAYER_PREFS_KNIFE_NUMBER, 0);
        SelectKnife(knifeNumber);

        for (int i = 0; i < knifeButtons.Count; i++)
        {
            int iCopy = i;
            knifeButtons[i].onClick.AddListener(() =>
            {
                SelectKnife(iCopy);
            });
        }
    }


    private void SelectKnife(int knifeIndex)
    {

            knifeSelecter.SelectKnife(knifeIndex);
            PlayerPrefs.SetInt(PLAYER_PREFS_KNIFE_NUMBER, knifeIndex);
            PlayerPrefs.Save();
    }
}
