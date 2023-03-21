using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button knifeMenuButton;
    [SerializeField] private Button homeMenuButton;
    [SerializeField] private Button trailMenuButton;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        knifeMenuButton.onClick.AddListener(() =>
        {
            OpenKnifeMenu();
        });

        homeMenuButton.onClick.AddListener(() =>
        {
            CloseKnifeMenu();
        });
    }

    private void OpenKnifeMenu()
    {
        animator.ResetTrigger("CloseKnifeMenu");
        animator.SetTrigger("OpenKnifeMenu");
    }

    private void CloseKnifeMenu()
    {
        animator.ResetTrigger("OpenKnifeMenu");
        animator.SetTrigger("CloseKnifeMenu");
    }
}
