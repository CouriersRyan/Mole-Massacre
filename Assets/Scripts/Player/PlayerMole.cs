using StarterAssets;
using UnityEngine;

// Handles dying, respawning, and ending the level.
public class PlayerMole : MonoBehaviour
{
    [SerializeField] private GameObject corpse;
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private int maxLives;

    private ThirdPersonController _controller;

    public int lives { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<ThirdPersonController>();
        lives = maxLives;
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
                GameManager.NextLevel();
                break;
        }
    }

    // Run when the player dies.
    private void Die()
    {
        // This lies the corpse on its belly
        Instantiate(corpse, transform.position, Quaternion.Euler(90, 0, 0));
        _controller.Teleport(respawnPoint, Quaternion.identity);
        lives--;
        if (lives < 0) GameManager.GameOver();
    }
}