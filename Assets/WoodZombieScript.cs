using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodZombieScript : MonoBehaviour
{
    public Transform Player;
    public float speed = 5.0f;
    void Update()
    {
        // Move the game object towards the player
        Vector3 direction = (Player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public Text LoseText;
    void OnCollisionEnter(Collision collision)
    {
        // Check the tag of the object the bullet collides with
        if (collision.collider.CompareTag("Wood"))
        {
           
            Debug.Log("You Lose");
            LoseText.gameObject.SetActive(true);
        }
        else if (collision.collider.CompareTag("Fire"))
        {
            Destroy(gameObject);
        }

    }
}
