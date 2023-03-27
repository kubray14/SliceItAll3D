using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public event Action OnClick;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                if (!GameManager.Instance.isGamePaused && GameManager.Instance.isGameStarted && !GameManager.Instance.isPlayerDead && !GameManager.Instance.isLevelFinished)
                {
                    OnClick?.Invoke();
                }
            }

        }
    }

}
