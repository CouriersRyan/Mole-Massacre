using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LivesCounter : MonoBehaviour
{
    private TMP_Text _text;
    private PlayerMole _player;

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