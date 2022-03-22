using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class HealthController : MonoBehaviour
{
    public int playerHealth;
    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(playerHealth);
    }

    public void UpdateHealth(int health)
    {
        for(int i = 0; i< hearts.Length; i++)
        {
             if(i< health)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.black;
            }
           
        }
    }
}
