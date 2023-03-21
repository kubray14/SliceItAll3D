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
        myScoreCanvas = Instantiate(scoreCanvasPrefab, transform.position, Quaternion.identity, transform); // Score canvas'imizi instantiate ediyoruz.
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

    public void ShowScoreCanvas(Vector3 position) // Kesilirken cagiriliyor. Canvas'i aciyor. Text'ini moneyvalue yapiyor. Parent'ini null yapiyor cünkü kesilirken bu obje destroy oluyor.
    {
        myScoreCanvas.transform.position = position;
        myScoreCanvas.SetActive(true);
        myScoreCanvas.gameObject.GetComponentInChildren<TMP_Text>().text = "+" + moneyValue.ToString();
        myScoreCanvas.transform.parent = null;
        myScoreCanvas.transform.localScale = Vector3.one;
    }
}
