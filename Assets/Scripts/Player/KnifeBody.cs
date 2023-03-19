using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBody : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable iHittable))
        {
            iHittable.Hit(playerController, false);
        }
    }
}
