using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeyController : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D col)
    {
            if (col.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Collision Key");
            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();

            Destroy(gameObject);

            playerController.PickUpKey();
           // Destroy(gameObject);
        }

    }

}
