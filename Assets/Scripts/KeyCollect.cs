using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour, IHittable
{
    private bool collected = false;
    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        if (!collected)
        {
            StartCoroutine(Collect_Coroutine());
            collected = true;
        }

    }

    private IEnumerator Collect_Coroutine()
    {
        this.enabled = false;
        float timeMax = 1f;
        float time = 0;
        Vector3 startScale = transform.localScale;
        while (true)
        {
            if (time >= timeMax)
            {
                GameManager.Instance.CollectKey();
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, time / timeMax);

        }
    }
}