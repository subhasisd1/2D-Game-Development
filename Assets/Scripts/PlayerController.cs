using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//[Serializable]
public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;

    public Animator animator;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider2D;
	public GameObject gameMainMenu;
    public GameObject scrorePanel;
    public GameObject gameWon;
    public float jump;
    [SerializeField]
    public bool isPGrounded;

    private bool isPlayerPlatform;
    private float speed;
    public Vector3 playerscale;
    public  Vector2 coliderSize, offsetSize;
    private bool isDead;
    private int currentScene;
    public GameObject platformPos;

    [SerializeField]
    public float horizontal;

    void Awake()
    {
 
        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        gameWon.SetActive(false);

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
        horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!isDead)
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
        scoreController.IncreaseScore(10);
    }
    private void playerMovement(float horizontal, float vertical)
    {

            animator.SetFloat("speed", Mathf.Abs(horizontal));
            playerscale = transform.localScale;

            if (horizontal < 0)
            {
                playerscale.x = -1f * Mathf.Abs(playerscale.x);
            }
            else if (horizontal > 0)
            {
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
         if (Input.GetKeyDown(KeyCode.Space) && isPGrounded)
        {
                rigidbody2.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                animator.SetBool("Jump", true);
            
             
        }else
        {
            animator.SetBool("Jump", false);
        }
    }

    public void KillPlayer()
    {
        isDead = true;
        animator.SetTrigger("isDead");

        StartCoroutine(PlayDeathCoroutine());
    }
    
    public void HitByEnemy()
    {

    }

    IEnumerator PlayDeathCoroutine()
    {      
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
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Door")
        {
            gameWon.SetActive(true);

        }

        if (col.gameObject.name == "GroundTileMap" ||
            col.gameObject.name == "MovingPlatform")
        {
            isPGrounded = true;
            FindObjectOfType<PlayerAudioController>().Play(SoundType.PlayerJumpLand);

        }

        if (col.gameObject.name == "DeathGround")
        {
            KillPlayer();
        }

        if (col.gameObject.name == "MovingPlatform")
        {
            isPlayerPlatform = true;
           // platformPos = col.gameObject.GetComponent<MovingPlatformController>().transform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "GroundTileMap" ||
            collision.gameObject.name == "MovingPlatform")
        {
            isPGrounded = false;
           FindObjectOfType<PlayerAudioController>().Play(SoundType.PlayerJump);

        }

        if (collision.gameObject.name == "MovingPlatform")
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

    public void NextLevel()
    {
        SceneManager.LoadScene(currentScene + 1);
    }
}
