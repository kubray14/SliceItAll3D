using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    [SerializeField] private List<Image> keysImageList;
    private void Start()
    {
        GameManager.Instance.OnCollectKey += GameManager_OnCollectKey;
        gameObject.SetActive(false);
    }

    private void GameManager_OnCollectKey()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < GameManager.Instance.collectedKeyCount; i++)
        {
            keysImageList[i].color = Color.yellow;
        }
    }

    private void Hide() // Animation event.
    {
        gameObject.SetActive(false);
    }
}
