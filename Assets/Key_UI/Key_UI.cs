using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_UI : MonoBehaviour
{
    private void Start()
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
