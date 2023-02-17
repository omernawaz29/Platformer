using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float speed;



    bool hit;
    float flyDirection;
    float timer;
    BoxCollider2D col;
    Animator anim;
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit)
            return;

        float moveSpeed = speed * Time.deltaTime* flyDirection;
        transform.Translate(moveSpeed, 0, 0);

        timer += Time.deltaTime;

        if (timer > 5f)
            DisableFireBall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.GetComponent<Collider2D>().isTrigger)
        {
            DisableFireBall();
        }
    }

    void DisableFireBall()
    {
        timer = 0;
        hit = true;
        col.enabled = false;
        anim.SetTrigger("explode");
    }

    public void Shoot(float direction)
    {
        gameObject.SetActive(true);
        hit = false;
        col.enabled = true;
        flyDirection = direction;

        transform.localScale = new Vector3(direction, 1, 1);

    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
