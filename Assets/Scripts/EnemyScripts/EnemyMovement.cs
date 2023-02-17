using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float moveSpeed;

    [Header("References")]
    [SerializeField] Transform playerCheck;
    [SerializeField] LayerMask playerLayer;

    Rigidbody2D rb;
    Animator anim;
    int direction;
    LevelController lvlControl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        lvlControl = FindObjectOfType<LevelController>();
        direction = 1;
    }

    private void FixedUpdate()
    {
        if (!lvlControl.moveEnemies)
            return;

        if (isPlayerInRange() && lvlControl.gameRunning)
        {
            rb.velocity = Vector2.zero;
            anim.Play("Attack");
        }
        else
        {
            rb.AddForce(Vector2.right * direction * moveSpeed, ForceMode2D.Force);
        }
    }



    bool isPlayerInRange()
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(playerCheck.position, 0.4f, Vector2.right * direction, 0, playerLayer);
        return hit;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "turnAround")
        {
            rb.velocity = Vector2.zero;
            direction = -direction;
            if(direction == 1)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);


        }
    }
}
