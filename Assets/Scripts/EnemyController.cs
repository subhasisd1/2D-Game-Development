using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float walkSpeed, range;
    private float disToPlayer;

    // [HideInInspector]
    public bool mustPatrol;
    public bool mustTurn;
    private bool enemyRun;
    public Rigidbody2D rb;
    public Transform groudChkedPos;
    public LayerMask groundLayer;   
    
    public Animator animator;
    public Collider2D bodyColider;
    public Transform player;
    public HealthController health;

    void Start()
    {
        mustPatrol = true;
        animator.SetBool("walk", true);
        enemyRun = false;

    }

    void Update()
    {
      
        if (mustPatrol)
        {
           Patrol();
        }

        disToPlayer = Vector2.Distance(transform.position, player.position);

        if(disToPlayer <= range)
        {
             animator.SetBool("run", true);
            enemyRun = true;

            if(player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0 )
            {
                Flip();
            }
        }
        else
        {
             animator.SetBool("run", false);
            enemyRun = false;

        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groudChkedPos.position, 0.1f, groundLayer);

        }
    }

    private void Patrol()
    {
     //   if (mustTurn || bodyColider.IsTouchingLayers(groundLayer))
        if (mustTurn)
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }


     private void OnCollisionEnter2D(Collision2D col)
    {
            if (col.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();

             health.playerHealth -= 1;
            if(health.playerHealth == 0)
            {
                 playerController.KillPlayer();
            }else
            {
                health.UpdateHealth(health.playerHealth);
            }
        }

            if(col.gameObject.tag == "GroundTileMap")
        {
            rb.gravityScale = 0f;
        }

    }

      void OnCollisionExit2D(Collision2D collision)
    {
  
     if(collision.gameObject.name == "GroundTileMap")
        {
         rb.gravityScale = 1f;
        }
    }


    private void RunToPLayer()
    {
         if(disToPlayer <= 2f)
                {
                    animator.SetBool("run", true);

                }else
                {
                    animator.SetBool("run", false);

                }
                
    }

}
