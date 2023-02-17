using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    [SerializeField] DoorElevatorScript door;
    HingeJoint2D hinge;

    private void Start()
    {
        hinge = GetComponentInChildren<HingeJoint2D>();
        StartCoroutine(DisableMotor());
    }

    private void Update()
    {
        if (hinge.jointAngle >= 55)
            door.Open();
        else if (hinge.jointAngle <= -55)
            door.Close();
    }

    IEnumerator DisableMotor()
    {
        yield return new WaitForSeconds(0.25f);
        hinge.useMotor = false;
    }
}
