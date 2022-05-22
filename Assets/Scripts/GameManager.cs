using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static bool paused { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindObjectOfType<GameManager>();
            if (_instance == null)
            {
                var empty = new GameObject();
                var manager = empty.AddComponent<GameManager>();
                _instance = manager;
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public void Quit()
    {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}