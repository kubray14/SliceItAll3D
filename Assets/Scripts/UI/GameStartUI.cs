using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private Button tapToPlayButton;

    private void Awake()
    {
        tapToPlayButton.onClick.AddListener(() =>
        {
            // Oyunu başlatıyoruz.
            GameManager.Instance.StartGame();
            gameObject.SetActive(false);
        });
    }
}
