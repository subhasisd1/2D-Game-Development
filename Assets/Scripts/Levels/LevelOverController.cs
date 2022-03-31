using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Levels
{
    public class LevelOverController : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Level Finished by the Player");

                FindObjectOfType<AudioManager>().Play("DoorOpening");

                 LevelManager.Instance.MarkCurrentLevelComplete();
            }
        }
    }
}