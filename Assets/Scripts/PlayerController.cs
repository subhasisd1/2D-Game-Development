using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//[Serializable]
public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;
    public GameObject objRockPair;
    public GameObject RlPlatform;
    public GameObject UDPlatform;
    public GameObject StblPlatform;

    public Animator animator;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider2D;
    public HealthController health;

    public GameObject gameMainMenu;
    public GameObject gameWon;
    public float jump;
    [SerializeField]
    public bool isPGrounded;

    private float playerSpeed;
    public Vector3 playerscale;
    public Vector2 coliderSize, offsetSize;
    public bool isDead;
    private int currentScene;
    public GameObject platformPos;

    [SerializeField]
    public float horizontal;
    private bool isPaused = false;

    private MovingPlatformController movingPlatform;
    private bool isCroutch = false;


    void Awake()
    {

        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Level : " + currentScene);
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        gameWon.SetActive(false);
        movingPlatform = FindObjectOfType<MovingPlatformController>();
        objRockPair.SetActive(false);
        RlPlatform.SetActive(false);
        UDPlatform.SetActive(false);
        StblPlatform.SetActive(false);

        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Started");
        playerSpeed = 5f;
        jump = 25f;
        coliderSize = boxCollider2D.size;
        offsetSize = boxCollider2D.offset;
    }

    void Update()
    {

        // Debug.Log("is Dead : " + isDead);

        horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!isDead && !isPaused && !isCroutch)
        {
            playerMovement(horizontal, vertical);
            MoveCharecter(horizontal, vertical);
            PlayerJump();

        }

        onExitButtonClick();
        PLayerCrouch();
        PlayerHurt();

        if (health.playerHealth == 0)
            KillPlayer();


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
        position.x = position.x + horizontal * playerSpeed * Time.deltaTime;
        transform.position = position;
    }
    private void PLayerCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", true);
            boxCollider2D.size = new Vector2(1f, 1.303408f);
            boxCollider2D.offset = new Vector2(-0.01289269f, 0.5362488f);
            isCroutch = true;
        } 
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            boxCollider2D.size = coliderSize;
            boxCollider2D.offset = offsetSize;
            animator.SetBool("Crouch", false);
            isCroutch = false;

        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPGrounded)
        {
            rigidbody2.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
        } else
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


    IEnumerator PlayDeathCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameMainMenu.SetActive(true);
      //  animator.enabled = false;
    }

    private void onExitButtonClick()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape is Clicked");

            if (!isPaused)
                isPaused = true;
            else
                isPaused = false;

            gameMainMenu.SetActive(isPaused);
            animator.enabled = !isPaused;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        string strCOl = col.gameObject.name;

       Debug.Log("Got Touched by " + strCOl);


        if (strCOl == "GroundTileMap" ||
            strCOl == "ExtraTileMap" ||
            strCOl == "Rock" ||
            strCOl == "MovingPlatform" ||
            strCOl == "MovingUpPlatform" && !isDead) {

            isPGrounded = true;
            playerSpeed = 5f;
            FindObjectOfType<PlayerAudioController>().Play(SoundType.PlayerJumpLand);

        }

        if (strCOl == "DeathGround")
        {
            KillPlayer();
        }

        if (strCOl == "Enemy" && !isDead)
        {
            PlayerDamage();
        }

        if (strCOl == "Spikes" && !isDead)
        {
            animator.SetTrigger("isHurt");
            PlayerDamage();
        }

        if (strCOl == "EnemyBullet" && !isDead)
        {
            PlayerDamage();
            animator.SetTrigger("isHurt");
        }

        

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        string strCOl = collision.gameObject.name;


        if (strCOl == "GroundTileMap" ||
            strCOl == "MovingPlatform" ||
            strCOl == "MovingUpPlatform" ||
            strCOl == "ExtraTileMap")
        {
            isPGrounded = false;
            playerSpeed = 4.5f ;
           FindObjectOfType<PlayerAudioController>().Play(SoundType.PlayerJump);

        }

        /*
        if (collision.gameObject.name == "MovingPlatform")
        {
            isPlayerPlatform = false;
        }
        */
    }

    public void RestartGame()
    {
        Debug.Log("Restart is Clicked");

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

    public void PlayerDamage()
    {
        health.playerHealth -= 1;
        health.UpdateHealth(health.playerHealth);

        if (health.playerHealth == 0)
            KillPlayer();
    }


    public void PlayerHurt()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetTrigger("isHurt");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        string strCOl = collision.gameObject.name;


        if (strCOl == "Switch 2" )
        {
            UDPlatform.SetActive(true);
        }

        if (strCOl == "Switch 1")
        {
            RlPlatform.SetActive(true);
        }

        if (strCOl == "Switch 3")
        {
            objRockPair.SetActive(true);
        }

        if (strCOl == "Switch 4")
        {
            StblPlatform.SetActive(true);
        }
    }
}
