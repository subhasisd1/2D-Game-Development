using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float walkSpeed, range;
    private float disToPlayer;

    public bool mustPatrol;
    public bool isPlayerInRange;


    [HideInInspector]
    public bool mustTurn;

    public Rigidbody2D rb;
    public Transform groudChkedPos;
    public LayerMask groundLayer;   
    public Animator animator;


    public Transform player;
    public HealthController health;

    void Start()
    {
        mustPatrol = true;
        animator.SetBool("walk", true);

    }

    void Update()
    {
        disToPlayer = Vector2.Distance(transform.position, player.position);

        if (mustPatrol)
        {
           Patrol();
        }

        if(disToPlayer <= range)
        {
             animator.SetBool("run", true);
            mustPatrol = true;
            // walkSpeed = 100f;
            isPlayerInRange = true;
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0 )
            {

                Flip();
            }
        }
        else
        {
             animator.SetBool("run", false);
            mustPatrol = true;
            isPlayerInRange = false;

            // walkSpeed = 50f;
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(groudChkedPos.position, Vector2.down, 0.1f);

            if (groundInfo.collider == false)
            {            
                Flip();
            }
        }
    }

    private void Patrol()
    {
        //if (mustTurn || bodyColider.IsTouchingLayers(groundLayer))



        if (mustTurn)
        {
            Flip();
        }
    
        transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);

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
            if (health.playerHealth == 0)
            {
                playerController.KillPlayer();
                health.UpdateHealth(health.playerHealth);
            }
            else
            {
                health.UpdateHealth(health.playerHealth);
                playerController.HitByEnemy();
            }
        }

        if (col.gameObject.tag == "GroundTileMap")
        {
            rb.gravityScale = 0f;
            Patrol();
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
