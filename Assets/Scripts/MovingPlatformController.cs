using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // needed for Tilemap

public class MovingPlatformController : MonoBehaviour
{
    public float walkSpeed, range;
    private float disToPlayer;
    public bool mustPatrol;
    public bool isPlayerInRange;
    public Tilemap tilemap;

    [HideInInspector]
    public bool mustTurn;

    public Transform groudChkedPos;
    public Collider2D bodyColider;
    public LayerMask groundLayer;

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
    }

    private void FixedUpdate()
    {
    }

    private void Patrol()
    {
        transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
    }

    void Flip()
    {
        mustPatrol = false;
        //  transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "GroundTileMap")
        {
            Debug.Log("Touched " + col.gameObject.name + " here");
            Flip();
        }
    }
}
