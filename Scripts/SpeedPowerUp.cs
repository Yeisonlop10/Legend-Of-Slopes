using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    private GameObject player;//reference to the player
    private PlayerController playerController; //reference to the player controller script

    // Use this for initialization
    void Start()
    {

        player = GameManager.instance.Player; //instantiate player
        playerController = player.GetComponent<PlayerController>(); //get reference to playercontroller
        GameManager.instance.RegisterPowerUp();//registerpowerup
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) //if collides with player
        {
            playerController.SpeedPowerUp();//call speedpowerup in player controller
            Destroy(gameObject);//destroy game object

        }
    }
}
