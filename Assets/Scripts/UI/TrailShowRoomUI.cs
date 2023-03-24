using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailShowRoomUI : MonoBehaviour
{
    private const string PLAYER_PREFS_TRAIL_NUMBER = "TrailNumber";
    [SerializeField] private List<Button> trailButtons;
    private int trailNumber;


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
        if (GameManager.Instance.activePlayerController != null)
        {
            List<GameObject> trails = GameManager.Instance.activePlayerController.trails;
            if (trailNumber <= trails.Count)
            {
                GameManager.Instance.activePlayerController.SelectTrail(trailNumber);
                PlayerPrefs.SetInt(PLAYER_PREFS_TRAIL_NUMBER, trailNumber);
                PlayerPrefs.Save();
            }
        }
    }

    public void SelectDefaultTrail()
    {
        if (GameManager.Instance.activePlayerController != null)
        {
            List<GameObject> trails = GameManager.Instance.activePlayerController.trails;
            trailNumber = PlayerPrefs.GetInt(PLAYER_PREFS_TRAIL_NUMBER, 0);
            if (trailNumber <= trails.Count)
            {
                GameManager.Instance.activePlayerController.SelectTrail(trailNumber);
                PlayerPrefs.SetInt(PLAYER_PREFS_TRAIL_NUMBER, trailNumber);
                PlayerPrefs.Save();
            }
        }
    }
}
