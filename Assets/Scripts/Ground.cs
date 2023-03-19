using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IHittable
{
    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        if (isSharpEdgeCollided)
        {
            playerController.Stuck();
        }
        else
        {
            playerController.PushBack();
        }
        
    }
}
