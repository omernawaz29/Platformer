using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] DoorElevatorScript door;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ninjaWarrior" || collision.tag == "dragonWarrior" || collision.tag == "box")
        {
            StartCoroutine(disableObject());
            anim.Play("buttonPress");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ninjaWarrior" || collision.tag == "dragonWarrior" || collision.tag == "box")
        {
            StartCoroutine(enableObject());
            anim.Play("buttonRelease");
        }
    }

    IEnumerator enableObject()
    {
        yield return new WaitForSeconds(0.3f);
        door.Close();

    }

    IEnumerator disableObject()
    {
        yield return new WaitForSeconds(0.3f);
        door.Open();

    }



}
