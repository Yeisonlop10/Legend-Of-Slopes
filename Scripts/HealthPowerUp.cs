using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    private GameObject player; //reference to player
    private PlayerHealth playerHealth; // reference to player health script

    // Use this for initialization
    void Start()
    {

        player = GameManager.instance.Player; //instantiate player
        playerHealth = player.GetComponent<PlayerHealth>(); // get player health script
        GameManager.instance.RegisterPowerUp();
    }

    void OnTriggerEnter(Collider other) //when player gets in contact with the sphere
    {
        if (other.gameObject == player) //if the object is the player
        {
            playerHealth.PowerUpHealth(); //call function player health
            Destroy(gameObject);//destory game object
        }
    }
}
