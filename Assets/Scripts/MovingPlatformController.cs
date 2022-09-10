using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // needed for Tilemap

public class MovingPlatformController : MonoBehaviour
{
    public float walkSpeed, range;
    public bool mustPatrol;
    public Tilemap tilemap;

    [HideInInspector]
    public bool mustTurn;

    public Rigidbody2D rigidbody2D;
    public Collider2D bodyColider;
    public LayerMask groundLayer;


    private void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

      
      //   Debug.Log(rigidbody2D.velocity);

    }

    private void Patrol()
    {
        if (gameObject.name.Equals("MovingUpPlatform"))
        {
            transform.Translate(Vector2.up * walkSpeed * Time.deltaTime);

        }
        else
        transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
    }

    void Flip()
    {
        mustPatrol = false;
        walkSpeed *= -1;
        mustPatrol = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
      //  Debug.Log("Touched = " + col.gameObject.name);
        string strCol = col.gameObject.name;

        if (strCol == "GroundTileMap" || strCol == "PlatformColider" || strCol == "Rock")
        {
            Flip();
        }

        if (strCol == "Player")
            col.collider.transform.SetParent(transform);
      //  Debug.Log(rigidbody2D.velocity);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        string strCol = collision.gameObject.name;

        if (strCol == "Player")
            collision.collider.transform.SetParent(null);
    }
}
