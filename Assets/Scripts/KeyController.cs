using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyController : MonoBehaviour
{

    public ParticleSystem keyPS;

   private void OnCollisionEnter2D(Collision2D col)
    {
            if (col.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
            FindObjectOfType<KeyPickUp>().Play("PickKey");
            playerController.PickUpKey();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
           
            StartCoroutine(CreatDust());
        }
    }

    IEnumerator CreatDust()
    {
        keyPS.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
