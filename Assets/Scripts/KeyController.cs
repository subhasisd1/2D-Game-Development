using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyController : MonoBehaviour
{

    public ParticleSystem keyPS;

    IEnumerator CreatDust()
    {
        keyPS.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log(collision.gameObject.name);

        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            FindObjectOfType<KeyPickUp>().Play("PickKey");
            playerController.PickUpKey();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            StartCoroutine(CreatDust());
        }
    }
}
