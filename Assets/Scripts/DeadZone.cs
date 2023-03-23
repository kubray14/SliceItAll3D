using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour, IHittable
{
    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        GameManager.Instance.PlayerDeath();
    }
}
