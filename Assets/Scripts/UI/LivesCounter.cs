using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LivesCounter : MonoBehaviour
{
    private PlayerMole _player;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _player = FindObjectOfType<PlayerMole>();
    }

    private void Update()
    {
        _text.text = GameManager.isGameOver ? "Out of lives!" : $"Lives: {_player.lives}";
    }
}