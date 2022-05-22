using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Awake()
    {
        Resume();
        InputSystem.onAfterUpdate += () =>
        {
            // doesn't work in Update() for some reason
            if (pauseButton.WasReleasedThisFrame())
            {
                if (GameManager.paused) Resume();
                else Pause();
            }
        };
    }

    public void Pause()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.Pause();
    }

    public void Resume()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.Resume();
    }
}