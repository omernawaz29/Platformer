using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDieTestScript : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] int enemyID;
    string attackType;

    private void Start()
    {
        if (enemyID == 0)
            attackType = "dragonBall";
        else if (enemyID == 1)
            attackType = "playerAttack";
        else
            attackType = "both";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == attackType || attackType == "both")
            Destroy(gameObject);
    }
}
