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
            OpenHomeMenu();
        });

        trailMenuButton.onClick.AddListener(() =>
        {
            OpenTrailMenu();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnLevelStart += GameManager_OnLevelStart;
    }

    private void GameManager_OnLevelStart()
    {
        Hide();
    }

    private void OpenKnifeMenu()
    {
        animator.SetBool("Close",false);
        animator.SetBool("OpenTrailMenu", false);
        animator.SetBool("OpenKnifeMenu",true);
    }

    private void OpenHomeMenu()
    {
        animator.SetBool("OpenTrailMenu",false);
        animator.SetBool("OpenKnifeMenu",false);
        animator.SetBool("Close", true);
    }

    private void OpenTrailMenu()
    {
        animator.SetBool("Close",false);
        animator.SetBool("OpenKnifeMenu", false);
        animator.SetBool("OpenTrailMenu", true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

  
}
