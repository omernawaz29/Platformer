using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform player2;
    void Update()
    {
        if (!player || !player2)
            return;
        float yMidPoint = (player.position.y + player2.position.y) * 0.5f;
        float xMidPoint = (player.position.x + player2.position.x) * 0.5f;
        transform.position = new Vector3(xMidPoint, yMidPoint, transform.position.z);
    }
}
