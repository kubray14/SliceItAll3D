using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float smoothFollowTime = 1.0f;

    private Vector3 offset;

    private void LateUpdate()
    {
        if (player != null && GameManager.Instance.isGameStarted)
        {
            SmoothFollowPlayer();
        }

    }

    private void SmoothFollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * smoothFollowTime);
    }

    private void FollowPlayer()
    {
        transform.position = player.position + offset;
    }

    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
        offset = transform.position - player.position;
    }
}
