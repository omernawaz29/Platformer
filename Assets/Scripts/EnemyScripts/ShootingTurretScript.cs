using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurretScript : MonoBehaviour
{
    [SerializeField] float coolDownTime;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject[] FireBalls;
    [SerializeField] LayerMask playerLayer;

    int direction;
    float timer = 0f;

    private void Start()
    {
        direction = (int)Mathf.Sign(transform.localScale.x);
    }


    private void Update()
    {
        if (timer > coolDownTime && playerInRange())
            ShootBall();
        timer += Time.deltaTime;
    }
    void ShootBall()
    {
        timer = 0;
        FireBalls[FindFireBall()].transform.position = firePoint.position;
        FireBalls[FindFireBall()].GetComponent<FireBall>().Shoot(transform.localScale.x);
    }

    int FindFireBall()
    {
        for (int i = 0; i < FireBalls.Length; i++)
        {
            if (FireBalls[i].activeInHierarchy == false)
                return i;
        }
        return 0;
    }

    bool playerInRange()
    {
        return Physics2D.Raycast(firePoint.position, new Vector2(direction, 0), 10f,playerLayer);
    }



}
