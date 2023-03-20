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
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.Instance.isGamePaused)
            {
                OnClick?.Invoke();
            }
        }
    }
}
