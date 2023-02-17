using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] [Range(0,2)] int trapID;
    string playerType;


    float timer;
    private void Start()
    {
        if (trapID == 0)
            playerType = "ninjaWarrior";
        else if (trapID == 1)
            playerType = "dragonWarrior";
        else if (trapID == 2)
            playerType = "both";

        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerType || playerType == "both")
            enableTrap(collision);
    }

    void enableTrap(Collision2D collision)
    {
        if (trapID == 1)
        {
            Destroy(gameObject);
            return;
        }
        else if (timer > 1f)
        {
            timer = 0;
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.Die();
            return;
        }
    }

}
