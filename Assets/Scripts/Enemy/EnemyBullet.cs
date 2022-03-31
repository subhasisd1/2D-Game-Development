using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float dieTime, damage;
    public GameObject dieEffect;
    public EnemyController enemyController;
    private float vector2X;

    private void Awake()
    {
        enemyController = FindObjectOfType<EnemyController>();
        vector2X = enemyController.transform.localScale.x;
    }
    void Start()
    {
        transform.localScale = new Vector3(vector2X, 1, 1);
    }

    // Update is called once per frame


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
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
