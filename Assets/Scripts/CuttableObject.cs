using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CuttableObject : MonoBehaviour, IHittable
{
    [SerializeField] private int moneyValue = 1;
    public bool isCuttingEqual = true; // True ise kesilen 2 parca yere dusuyor. Else ise sol taraftaki parca sabit kaliyor.
    [SerializeField] private GameObject scoreCanvasPrefab;
    private GameObject myScoreCanvas;


    private void Start()
    {
        myScoreCanvas = Instantiate(scoreCanvasPrefab, transform.position, Quaternion.identity, transform);
        myScoreCanvas.SetActive(false);
    }

    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        if (isSharpEdgeCollided)
        {
            SoundManager.Instance.PlayCutSound();
            MoneyManager.Instance.AddMoney(moneyValue);

        }
        else
        {
            // Býçaðýn sapý kesilen obje ile trigger oldu.
            playerController.PushBack();
        }
    }

    public void ShowScoreCanvas()
    {
        myScoreCanvas.SetActive(true);
        myScoreCanvas.gameObject.GetComponentInChildren<TMP_Text>().text = "+" + moneyValue.ToString();
        myScoreCanvas.transform.parent = null;
        myScoreCanvas.transform.localScale = Vector3.one;
    }
}
