using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float dieTime, damage;
    public GameObject dieEffect;
    private EnemyController enemyController;
    private PlayerController playerController;
    private float vector2X;

    private void Awake()
    {
        enemyController = FindObjectOfType<EnemyController>();
        playerController = FindObjectOfType<PlayerController>();

        vector2X = enemyController.transform.localScale.x;
    }
    void Start()
    {
        transform.localScale = new Vector3(vector2X, 1, 1);
        StartCoroutine(CountDownTimer());

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Touched "+collision.gameObject.name);
        string cols = collision.gameObject.name;
        if (cols == "GroundTileMap" || cols == "ExtraTileMap")
            Die();

        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            playerController.animator.SetTrigger("isHurt");
           playerController.PlayerDamage();
           Die();
        }
    }


    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die()
    {
        Destroy(gameObject);

    }
}
