using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorScript : MonoBehaviour
{
    [SerializeField] [Range(0,1)] int doorID;

    Color DragonWarriorColor;
    Color NinjaWarriorColor;
    SpriteRenderer spriteRenderer;
    LevelController lvlControl;
    Animator anim;
    string playerType;

    void Start()
    {

        lvlControl = FindObjectOfType<LevelController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();


        //dragonWarriorHardCode;
        DragonWarriorColor.r = 255;
        DragonWarriorColor.g = 196;
        DragonWarriorColor.b = 0;
        DragonWarriorColor.a = 255;

        //ninjaWarriorHardCode
        NinjaWarriorColor.r = 255;
        NinjaWarriorColor.g = 0;
        NinjaWarriorColor.b = 210;
        NinjaWarriorColor.a = 255;

        if (doorID == 0)
        {
            spriteRenderer.color = DragonWarriorColor;
            playerType = "dragonWarrior";
        }
        else if (doorID == 1)
        {
            spriteRenderer.color = NinjaWarriorColor;
            playerType = "ninjaWarrior";
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerType)
        {
            anim.Play("doorOpen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == playerType)
        {
            lvlControl.playerExitDoor();
            anim.Play("doorClose");
        }
    }

    public void DoorOpen()
    {
        lvlControl.playerAtDoor();
    }



}
