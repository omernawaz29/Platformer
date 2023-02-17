using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] Sprite[] pickUps;
    [SerializeField] [Range(0,6)] int pickUpID;


    public int id { get { return pickUpID; } }


    LevelController lvlControl;
    SpriteRenderer SR;
    string playerType;

    private void Start()
    {
        SR = GetComponentInChildren<SpriteRenderer>();
        lvlControl = FindObjectOfType<LevelController>();

        if (pickUpID == 0)
        {
            playerType = "dragonWarrior";

        }
        else if (pickUpID == 1)
        {
            playerType = "ninjaWarrior";
        }
        else
        {
            playerType = "both";
        }
        SR.sprite = pickUps[pickUpID];
        lvlControl.PickUpAdd(pickUpID);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerType || playerType == "both")
        {
            lvlControl.PickUpCollected(pickUpID);
            Destroy(gameObject);
        }
    }




}
