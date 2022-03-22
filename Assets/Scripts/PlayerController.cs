using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;

    public Animator animator;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider2D;

	public GameObject gameMainMenu;
    public GameObject scrorePanel;

    public float jump;
    private bool isGrounded;
    private bool isPlayerPlatform;
    private float speed;
    public Vector3 playerscale;
    public  Vector2 coliderSize, offsetSize;
    private bool isDead;

    private int currentScene;

    public GameObject platformPos;


    void Awake()
    {
 
        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Started");
        speed = 5f;
        jump = 25f;
         coliderSize = boxCollider2D.size;
        offsetSize = boxCollider2D.offset;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (animator.enabled && !isDead)
        {
            playerMovement(horizontal, vertical);
            MoveCharecter(horizontal, vertical);
            PLayerCrouch();
            PlayerJump();
            onExitButtonClick();
        }

        if (isPlayerPlatform)
        {
       
           // transform.position = movingPlatform.transform.position;
        }
    }

    public void PickUpKey()
    {
        Debug.Log("Player Picked up the key");
        scoreController.IncreaseScore(10);
    }
    private void playerMovement(float horizontal, float vertical)
    {

         animator.SetFloat("speed", Mathf.Abs(horizontal));
        playerscale = transform.localScale;

        if(horizontal < 0) {
            playerscale.x = -1f * Mathf.Abs(playerscale.x);
        }
        else if(horizontal>0) {
            playerscale.x = Mathf.Abs(playerscale.x);
        }
       
        transform.localScale = playerscale;
    }

      private void MoveCharecter(float horizontal, float vertical)
    {
        Vector3 position = transform.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;
    }
    private void PLayerCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", true);          
            boxCollider2D.size = new Vector2(1f, 1.303408f);
            boxCollider2D.offset = new Vector2(-0.01289269f, 0.5362488f);

        }else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
             boxCollider2D.size = coliderSize;
             boxCollider2D.offset = offsetSize;
            
            animator.SetBool("Crouch", false);
        }     
    }

    private void PlayerJump()
    {
         if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse );
            animator.SetBool("Jump", true);   
        }else
        {
            animator.SetBool("Jump", false);
        }
    }

    public void KillPlayer()
    {
        isDead = true;
         StartCoroutine(PlayDeathCoroutine());
    }
    
    public void HitByEnemy()
    {
    }

    IEnumerator PlayDeathCoroutine()
    {
        // animator.Play("PlayerDeath");
        animator.SetBool("dead", true);
         yield return new WaitForSeconds(0.60f);
        animator.SetBool("dead", false);
       yield return new WaitForSeconds(1f);

         gameMainMenu.SetActive(true);
    }

    private void onExitButtonClick()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape is Clicked");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.name == "Door")
        {
          SceneManager.LoadScene(currentScene + 1);
        }

        if (col.gameObject.name == "GroundTileMap")
        {
            isGrounded = true;
        }

        if (col.gameObject.name == "DeathGround")
        {
            SceneManager.LoadScene(currentScene);
        }

        if (col.gameObject.name == "MovingPlatfotm")
        {
            isPlayerPlatform = true;
           // platformPos = col.gameObject.GetComponent<MovingPlatformController>().transform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "GroundTileMap")
        {
            isGrounded = false;
        }

        if (collision.gameObject.name == "MovingPlatfotm")
        {
            isPlayerPlatform = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
