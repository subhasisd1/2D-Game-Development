using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWon : MonoBehaviour
{
    public ParticleSystem gameWonPs;
    public GameObject gameWon;


    // Start is called before the first frame update
    void Awake()
    {
        gameWonPs.Stop();
        gameWon.SetActive(false);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        string strCOl = collision.gameObject.name;
        Debug.Log(strCOl);

        if (strCOl == "Player")
        {
            gameWonPs.Play();
            StartCoroutine(GameMenu());

        }
    }

    IEnumerator GameMenu()
    {
        yield return new WaitForSeconds(2f);
        gameWon.SetActive(true);
    }
}
