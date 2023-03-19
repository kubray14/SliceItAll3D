using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour, IHittable
{
    public void Hit(PlayerController playerController, bool isSharpEdgeCollided)
    {
        if (isSharpEdgeCollided)
        {
            PlayParticleEffect();
            PlayCutSound();
        }
        else
        {
            // Býçaðýn sapý kesilen obje ile trigger oldu.
            playerController.PushBack();
        }
    }

    private void PlayParticleEffect() // Olursa kesme efekti
    {
        print("Particle Effect");
    }

    private void PlayCutSound() // Kesme Sesi
    {
        print("Cut Sound");
    }
}
