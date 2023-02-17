using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] [Range(0,2)] int checkpointID;

    LevelController lvlControl;
    string playerType;

    private void Start()
    {
        lvlControl = FindObjectOfType<LevelController>();

        if (checkpointID == 0)
        {
            playerType = "dragonWarrior";

        }
        else if (checkpointID == 1)
        {
            playerType = "ninjaWarrior";
        }
        else if (checkpointID == 2)
        {
            playerType = "both";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerType || playerType == "both")
        {
            lvlControl.SetCheckPoint(checkpointID,transform.position);
        }
    }


}
