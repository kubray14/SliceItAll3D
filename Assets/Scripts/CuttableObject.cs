using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour, IHittable
{
    [SerializeField] private int moneyValue = 1;
    public bool isCuttingEqual = true; // True ise kesilen 2 parca yere dusuyor. Else ise sol taraftaki parca sabit kaliyor.


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
}
