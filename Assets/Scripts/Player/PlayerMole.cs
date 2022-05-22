using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Handles dying, respawning, and ending the level.
public class PlayerMole : MonoBehaviour
{
    [SerializeField] private GameObject corpse;
    [SerializeField] private Vector3 respawnPoint;
    // TODO: Retrieve max lives from level data somewhere instead of from inspector
    [SerializeField] private int maxLives;

    private ThirdPersonController _controller;
    private CharacterController _characterController;

    public int lives { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<ThirdPersonController>();
        _characterController = GetComponent<CharacterController>();
        lives = maxLives;
    }

    // Run when the player dies.
    void Die()
    {
        Instantiate(corpse, transform.position, transform.rotation);
        _controller.Teleport(respawnPoint, Quaternion.identity);
        lives--;
        if (lives <= 0)
        {
            // TODO: Add an actual game over screen (maybe allow player to restart)
            Debug.Log("Game over!");
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
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