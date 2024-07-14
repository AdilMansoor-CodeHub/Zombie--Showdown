using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireZombieScript : MonoBehaviour
{

    public Text LoseText;
    public float speed = 5.0f;

    public Transform Player;
    void Update()
    {
        // Move the game object towards the player
        Vector3 direction = (Player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check the tag of the object the bullet collides with
        if (collision.collider.CompareTag("Fire"))
        {
          
            Debug.Log("You Lose");

            LoseText.gameObject.SetActive(true);
        }
        else if (collision.collider.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
        
    }
}
