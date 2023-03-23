using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSelecter : MonoBehaviour
{
    [SerializeField] private List<GameObject> knifeList;
    private GameObject myKnife;
    private CameraController myCameraController;

    private void Start()
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
        myCameraController.SetPlayer(myKnife.transform);
        GameManager.Instance.SetActiveKnife(myKnife.GetComponent<PlayerController>());
    }
}
