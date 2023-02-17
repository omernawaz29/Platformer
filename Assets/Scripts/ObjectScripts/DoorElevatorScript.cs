using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorElevatorScript : MonoBehaviour
{
    [SerializeField] Vector3 openPos;
    [SerializeField] Vector3 closedPos;
    [SerializeField] float moveSpeed = 3;
    bool open;
    Transform playerTransform;
    private void Start()
    {
        transform.position = closedPos;
        open = false;
        playerTransform = null;
    }

    private void Update()
    {
        if (open)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, moveSpeed * Time.deltaTime);
            if(playerTransform && !isStopped())
                playerTransform.position = Vector3.Lerp(playerTransform.position, openPos, moveSpeed * Time.deltaTime);
        }
        else if(!open)
        {
            transform.position = Vector3.Lerp(transform.position, closedPos, moveSpeed * Time.deltaTime);
            if (playerTransform && !isStopped())
                playerTransform.position = Vector3.Lerp(playerTransform.position, closedPos, moveSpeed * Time.deltaTime);
        }

       

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
            playerTransform = collision.gameObject.GetComponent<Transform>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerTransform = null;
    }

    bool isStopped()
    {
        return transform.position == closedPos || transform.position == openPos;
    }
    public void Open()
    {
        open = true;
    }

    public void Close()
    {
        open = false;
    }

}
