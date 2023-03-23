using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundVolumeButton;
    [SerializeField] private TMP_Text soundVolumeText;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        soundVolumeText.text = "Sound Volume : " + SoundManager.Instance.GetVolume();
        soundVolumeButton.onClick.AddListener(() =>
        {
            ChangeSoundVolume();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            GameManager.Instance.ResumeGame();
            Time.timeScale = 1.0f;
        });

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ChangeSoundVolume()
    {
        SoundManager.Instance.ChangeSoundVolume();
        soundVolumeText.text = "Sound Volume : " + (int)SoundManager.Instance.GetVolume();
    }
}
