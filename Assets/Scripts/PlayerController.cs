using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;

    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider2D;

    public GameObject gameWonPanel;
	public GameObject gameLostPanel;

	public GameObject gameMainMenu;
    public GameObject gameLevel;

    public GameObject scrorePanel;
    public GameObject gameLevel1;



    public float jump;
    public bool isGrounded;
    public float speed;
    public Vector3 scale;
    public  Vector2 coliderSize, offsetSize;


    void Awake()
    {
        // gameMainMenu.SetActive(true);
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
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

        if (animator.enabled)
        {
            playerMovement(horizontal, vertical);
            MoveCharecter(horizontal, vertical);
            PLayerCrouch();
            PlayerJump();
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
         scale = transform.localScale;

        if(horizontal < 0) {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(horizontal>0) {
            scale.x = Mathf.Abs(scale.x);
        }
       
        transform.localScale = scale;

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
            rigidbody2D.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse );
            animator.SetBool("Jump", true);   
        }else
        {
            animator.SetBool("Jump", false);

        }

    }

    void OnCollisionEnter2D(Collision2D col)
     {
        
        if(col.gameObject.name == "Door")
        {
            gameWonPanel.SetActive (true);
            animator.enabled = false;
        }

         if(col.gameObject.name == "GroundTileMap")
        {
            isGrounded = true;

        }
     }

    public void RestartGame()
	{
		gameWonPanel.SetActive (false);
		gameLostPanel.SetActive (false);

		SceneManager.LoadScene (0);
	}

    void OnCollisionExit2D(Collision2D collision)
    {
  
     if(collision.gameObject.name == "GroundTileMap")
        {
        isGrounded = false;
        }
    }

    public void KillPlayer()
    {
         Debug.Log("PLayer Killed by Enemy");
		gameLostPanel.SetActive (true);
        animator.enabled = false;

    }

    public void StartGame()
    {
        gameMainMenu.SetActive(false);
        gameLevel1.SetActive(true);
        scrorePanel.SetActive(true);
    }

    public void showLevels()
    {
        gameLevel.SetActive(true);
        scrorePanel.SetActive(false);
        gameMainMenu.SetActive(false);
    }

    public void LoadGameLevel1()
    {
        gameLevel1.SetActive(true);
        gameMainMenu.SetActive(false);
        gameLevel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        gameMainMenu.SetActive(true);
        gameLevel.SetActive(false);

    }
}
