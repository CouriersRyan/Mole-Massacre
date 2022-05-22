using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

// Handles dying, respawning, and ending the level.
public class PlayerMole : MonoBehaviour
{
    [SerializeField] private GameObject corpse;
    [SerializeField] private Vector3 respawnPoint;

    private ThirdPersonController _controller;
    private CharacterController _characterController;

    private void Start()
    {
        _controller = GetComponent<ThirdPersonController>();
        _characterController = GetComponent<CharacterController>();
    }

    // Run when the player dies.
    void Die()
    {
        Instantiate(corpse, transform.position, transform.rotation);
        _controller.Teleport(respawnPoint, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        switch (tag)
        {
            case "Death":
                Die();
                break;
            case "Checkpoint":
                respawnPoint = other.transform.position;
                break;
            case "Finish":
                Debug.Log("Level End!"); 
                // TODO: Call an event from Game Manager to open the end of level menu.
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var tag = hit.gameObject.tag;
        switch (tag)
        {
            case "Death":
                Die();
                break;
        }
    }
}