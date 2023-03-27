using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private Button tapToPlayButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject optionsCanvas;

    private void Start()
    {
        tapToPlayButton.onClick.AddListener(() =>
        {
            // Oyunu baþlatýyoruz.
            GameManager.Instance.StartGame();
            gameObject.SetActive(false);
        });

        optionsButton.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
            GameManager.Instance.PauseGame();
            optionsCanvas.SetActive(true);
        });
    }
}
