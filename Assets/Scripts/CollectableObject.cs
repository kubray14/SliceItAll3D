using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour, IHittable
{
    private bool canHit = true;

    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        if (canHit)
        {
            if (isSharpEdgeCollided)
            {
                StartCoroutine(Collect_Coroutine());
                canHit = false;
            }
            else
            {
                playerController.PushBack();
            }
        }
    }

    private IEnumerator Collect_Coroutine()
    {
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
