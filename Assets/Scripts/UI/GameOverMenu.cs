using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class GameOverMenu : MonoBehaviour
{
    private void ShowMenu()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        GameManager.Pause();
    }

    private void Start()
    {
        GameManager.OnGameOver += ShowMenu;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= ShowMenu;
    }
}