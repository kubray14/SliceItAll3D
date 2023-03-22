using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailShowRoomUI : MonoBehaviour
{
    private const string PLAYER_PREFS_TRAIL_NUMBER = "TrailNumber";
    [SerializeField] private List<Button> trailButtons;
    [SerializeField] private List<GameObject> trails;
    private PlayerController playerController;

    private int trailNumber;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        trailNumber = PlayerPrefs.GetInt(PLAYER_PREFS_TRAIL_NUMBER, 0);
        SelectTrail(trailNumber);

        for (int i = 0; i < trailButtons.Count; i++)
        {
            int iCopy = i;
            trailButtons[i].onClick.AddListener(() =>
            {
                SelectTrail(iCopy);
            });
        }
    }

    private void SelectTrail(int trailNumber)
    {
        if (trailNumber <= trails.Count)
        {
            playerController.SelectTrail(trails[trailNumber]);
            PlayerPrefs.SetInt(PLAYER_PREFS_TRAIL_NUMBER, trailNumber);
            PlayerPrefs.Save();
        }
    }


}
