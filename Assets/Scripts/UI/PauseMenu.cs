using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class PauseMenu : MonoBehaviour
{
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
        Resume();
        InputSystem.onAfterUpdate += () =>
        {
            // doesn't work in Update() for some reason
            if (pauseButton.WasReleasedThisFrame())
            {
                if (GameManager.isPaused) Resume();
                else Pause();
            }
        };
    }

    public void Pause()
    {
        if (GameManager.isGameOver) return;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        GameManager.Pause();
    }

    public void Resume()
    {
        if (GameManager.isGameOver) return;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        GameManager.Resume();
    }
}