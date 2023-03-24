using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour, IHittable
{
    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        if (isSharpEdgeCollided)
        {
            StartCoroutine(Collect_Coroutine());
        }
        else
        {
            playerController.PushBack();
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
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, time / timeMax);

        }
    }
}
