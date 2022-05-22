using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    ///     Contains the input needed to trigger the pause menu to appear.
    /// </summary>
    [SerializeField] private InputAction pauseButton;

    private void OnEnable()
    {
        pauseButton.Enable();
    }

    private void OnDisable()
    {
        pauseButton.Disable();
    }

    private void Start()
    {
        CloseMenu();
        InputSystem.onAfterUpdate += () =>
        {
            // doesn't work in Update() for some reason
            if (pauseButton.WasReleasedThisFrame())
            {
                if (GameManager.isPaused) CloseMenu();
                else ShowMenu();
            }
        };
    }

    /// <summary>
    ///     Pauses the game and shows the pause menu
    /// </summary>
    public void ShowMenu()
    {
        if (GameManager.isGameOver) return;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        GameManager.Pause();
    }

    /// <summary>
    ///     Resumes the game and hides the pause menu
    /// </summary>
    public void CloseMenu()
    {
        if (GameManager.isGameOver) return;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        GameManager.Resume();
    }
}