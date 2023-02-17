using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] float attackCoolDown;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject[] FireBalls;

    PlayerMovement move;
    Animator anim;
    float coolDownTimer = Mathf.Infinity;

    private void Start()
    {
        move = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (move.pID == 1)
        {
            if (Input.GetKeyDown(KeyCode.F))
                NinjaAttack();
        }
        else if (move.pID == 0)
        {
            if (Input.GetKeyDown(KeyCode.RightControl) && coolDownTimer > attackCoolDown && move.canAttack())
                DragonWarriorAttack();
        }

        coolDownTimer += Time.deltaTime;
    }

    void NinjaAttack()
    {
        anim.Play("Attack");
    }

    void DragonWarriorAttack()
    {
        coolDownTimer = 0;
        anim.Play("Attack");

        FireBalls[FindFireBall()].transform.position = firePoint.position;
        FireBalls[FindFireBall()].GetComponent<FireBall>().Shoot(transform.localScale.x);
    }

    int FindFireBall()
    {
        for(int i = 0; i < FireBalls.Length;i++)
        {
            if (FireBalls[i].activeInHierarchy == false)
                return i;
        }
        return 0;

    }
}
