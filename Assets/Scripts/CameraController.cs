using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if(player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        transform.position = player.position + offset;
    }
}
