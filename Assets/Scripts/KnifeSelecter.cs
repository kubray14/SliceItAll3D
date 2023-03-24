using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSelecter : MonoBehaviour
{
    [SerializeField] private List<GameObject> knifeList;
    private GameObject myKnife;
    private CameraController myCameraController;

    private void Awake()
    {
        myCameraController = FindObjectOfType<CameraController>();
    }

    public void SelectKnife(int knifeIndex)
    {
        foreach (var knife in knifeList)
        {
            knife.SetActive(false);
        }
        myKnife = knifeList[knifeIndex];
        myKnife.gameObject.SetActive(true);
        GameManager.Instance.SetActiveKnife(myKnife.GetComponent<PlayerController>());
        myCameraController.SetPlayer(GameManager.Instance.GetActiveKnife());
    }
}
