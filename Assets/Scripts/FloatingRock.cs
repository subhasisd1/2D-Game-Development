using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingRock : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;
    // Start is called before the first frame update

    private void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string strCol = collision.gameObject.name;
       // Debug.Log(strCol);

        if (strCol == "Player")
            StartCoroutine(WaitForRockFall());
        if (strCol == "GroundTileMap")
        {
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }


        if (strCol == "DeathGround")
            Destroy(gameObject);
     //   Debug.Log(rigidbody2D.bodyType);

    }

    IEnumerator WaitForRockFall()
    {
        yield return new WaitForSeconds(0.5f);
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

    }
}
