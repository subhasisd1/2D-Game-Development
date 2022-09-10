using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float walkSpeed, range, timeBTWShots, shootSpeed;
    private float disToPlayer;
    private bool mustPatrol, cantShoot;
    public bool isPlayerInRange;
    [HideInInspector]
    public bool mustTurn;
    public Rigidbody2D rb;
    public Transform groudChkedPos;
    public LayerMask groundLayer;   
    public Animator animator;
    public Transform player, shootPos;
    public HealthController health;
    public GameObject bullet;
    public PlayerController playerController;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

    }

    void Start()
    {
        mustPatrol = true;
        cantShoot = true;
        animator.SetBool("walk", true);

    }

    void Update()
    {
        disToPlayer = Vector2.Distance(transform.position, player.position);

        if (mustPatrol)
        {
           Patrol();
        }

      //  Debug.Log("Is Dead : " + playerController.isDead);

        if(disToPlayer <= range && !playerController.isDead)
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

            mustPatrol = false;
            rb.velocity = Vector2.zero;
            if (cantShoot)
                StartCoroutine(Shoot());
        }
        else
        {
             animator.SetBool("run", false);
            mustPatrol = true;
            isPlayerInRange = false;
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
            playerController = col.gameObject.GetComponent<PlayerController>();
            if (health.playerHealth == 0)
            {
                playerController.KillPlayer();
                health.UpdateHealth(health.playerHealth);
            }
            else
            {
                health.playerHealth -= 1;
                health.UpdateHealth(health.playerHealth);
            }
        }

        if (col.gameObject.tag == "GroundTileMap")
        {
            rb.gravityScale = 0f;
            Patrol();
        }

        if (col.gameObject.name == "ExtraTileMap")
        {
            Flip();
        }
    }

    public void BulletHit()
    {
        health.playerHealth -= 1;
        health.UpdateHealth(health.playerHealth);

        if (health.playerHealth == 0)
            playerController.KillPlayer();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
  
     if(collision.gameObject.name == "GroundTileMap")
        {
         rb.gravityScale = 1f;
        }
    }

    IEnumerator Shoot()
    {
        cantShoot = false;
        yield return new WaitForSeconds(timeBTWShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

       newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * shootSpeed, transform.localScale.y);
      //  newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);

       // Debug.Log("Shotting Bullet : " + transform.localScale.x);

        cantShoot = true;

    }


}
