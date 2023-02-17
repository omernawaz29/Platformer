using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movment Variables")]
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpVelocityCap;
    [SerializeField] float speedCap;
    [SerializeField] float distanceAwayAllowed = 24;
    [SerializeField] [Range(0, 1)] int playerID;


    [Header("Object References")]
    [SerializeField] GameObject groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform friendPos;
    [SerializeField] HealthBar healthBar;




    public int pID { get { return playerID; } }


    //privates
    Rigidbody2D rb;
    Animator anim;
    LevelController lvlControl;
    float horizontalIn;
    float verticalIn;
    float multiplier;
    bool onLadder;
    float playerHealth;
    int jumpDir;
    bool healing;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        lvlControl = FindObjectOfType<LevelController>();

        multiplier = 10;
        onLadder = false;
        playerHealth = 100f;
        healthBar.SetHealth(playerHealth);
        jumpDir = 1;
        healing = false;


    }

    private void Update()
    {
        if(lvlControl.gameRunning)
            GetInput();

        AnimationControls();

        if (playerID == 1 && Input.GetKeyDown(KeyCode.G) && lvlControl.potions > 0 && !healing)
        {
            StartCoroutine(Heal()); 
            lvlControl.potions--;
            lvlControl.UpdateText();
        }
        if (playerID == 0 && Input.GetKeyDown(KeyCode.RightShift) && lvlControl.potions > 0 && !healing)
        {
            StartCoroutine(Heal());
            lvlControl.potions--;
            lvlControl.UpdateText();
        }

    }

    private void FixedUpdate()
    {
        if(lvlControl.gameRunning)
            Movement();
    }

    void GetInput()
    {
        if (playerID == 1)
        {
            horizontalIn = Input.GetAxisRaw("HorizontalB");
            verticalIn = Input.GetAxisRaw("VerticalB");

            
        }
        else if (playerID == 0)
        {
            horizontalIn = Input.GetAxisRaw("HorizontalA");
            verticalIn = Input.GetAxisRaw("VerticalA");
           
        }
    }
    void Movement()
    {
        //fix for getting stuck on walls
        if (!isGrounded() && rb.velocity.magnitude <= 1f)
        {
            horizontalIn = 0;
        }

        //movement
        if (horizontalIn == 1 && canMove())
        {
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
            rb.AddForce(Vector2.right * playerSpeed * multiplier, ForceMode2D.Force);
        }
        else if (horizontalIn == -1 && canMove())
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
            rb.AddForce(Vector2.left * playerSpeed * multiplier, ForceMode2D.Force);
        }


        if (verticalIn == 1 && isGrounded() && !onLadder)
        {
            rb.AddForce(Vector2.up * jumpForce * jumpDir, ForceMode2D.Impulse);
        }
        if (verticalIn == 1 && onLadder)
        {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


        //velocity clamps
        if (Mathf.Abs(rb.velocity.x) >= speedCap)
        {
            int direction = (int)Mathf.Sign(rb.velocity.x);
            rb.velocity = new Vector2(speedCap * direction, rb.velocity.y);
        }
        if (Mathf.Abs(rb.velocity.y) >= jumpVelocityCap)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, jumpVelocityCap);
        }
    }
    void AnimationControls()
    {
        if (rb.velocity.y >= 1 && !isGrounded())
            anim.SetTrigger("Jump");
        else if (Mathf.Abs(rb.velocity.x) >= 1)
            anim.SetTrigger("Run");
        if (rb.velocity.magnitude <= 1f)
            anim.SetTrigger("Stop");
    }
    bool isGrounded()
    {
        return Physics2D.CircleCast(groundCheck.transform.position, 0.1f, Vector2.down, 0f, groundLayer);
    }
    public bool canAttack()
    {
        return horizontalIn == 0 && isGrounded();
    }


    bool canMove()
    {
        return inFrame() || isMovingInward();
    }
    bool inFrame()
    {
        float distance = Vector3.Distance(transform.position, friendPos.position);
        return distance < distanceAwayAllowed;
    }

    bool isMovingInward()
    {
        int friendDir = (int)Mathf.Sign(friendPos.position.x - transform.position.x);
        return horizontalIn == friendDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemyAttack")
        {
            playerHealth -= 20f;
            healthBar.SetHealth(playerHealth);
            if (playerHealth <= 0)
                Die();
        }
        if(collision.tag == "ladder")
        {
            onLadder = true;
        }
        if(collision.tag == "pickUp")
        {
            int pickId = collision.gameObject.GetComponent<PickUpScript>().id;
            if (pickId == 3)
                StartCoroutine(SpeedUp());
            else if (pickId == 4)
                StartCoroutine(JumpUp());
            else if (pickId == 5)
                StartCoroutine(AntiGravity());
            else if (pickId == 6)
                StartCoroutine(StopEnemies());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ladder")
            onLadder = false;
    }


    public void Die()
    {
        anim.Play("Die");
        lvlControl.gameRunning = false;
        StartCoroutine(DieAnimWait());
    }   
    
    IEnumerator DieAnimWait()
    {
        yield return new WaitForSeconds(0.6f);
        lvlControl.PlayerDied(gameObject, playerID);
        playerHealth = 100f;
        healthBar.SetHealth(playerHealth);
    }

    IEnumerator Heal()
    {
        healing = true;
        while(playerHealth < 100)
        {
            playerHealth++;

            if (playerHealth > 100)
                playerHealth = 100;

            healthBar.SetHealth(playerHealth);
            yield return new WaitForSeconds(0.05f);
        }
        healing = false;
    }

    IEnumerator SpeedUp()
    {
        playerSpeed *= 2f;
        speedCap *= 1.5f;
        yield return new WaitForSeconds(3f);
        playerSpeed /= 2f;
        speedCap /= 1.5f;
    }

    IEnumerator JumpUp()
    {
        jumpForce *= 1.5f;
        jumpVelocityCap *= 1.5f;
        yield return new WaitForSeconds(3f);
        jumpForce /= 1.5f;
        jumpVelocityCap /= 1.5f;
    }

    IEnumerator AntiGravity()
    {
        rb.gravityScale = -rb.gravityScale;
        jumpDir = -1;
        transform.localScale = new Vector3(transform.localScale.x, -1, 1);
        yield return new WaitForSeconds(5f);
        rb.gravityScale = -rb.gravityScale;
        jumpDir = 1;
        transform.localScale = new Vector3(transform.localScale.x, 1, 1);
    }

    IEnumerator StopEnemies()
    {
        lvlControl.moveEnemies = false;
        yield return new WaitForSeconds(5f);
        lvlControl.moveEnemies = true;
    }

}
