using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static bool isPaused { get; private set; }
    public static bool isGameOver { get; private set; }

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

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        isPaused = false;
        isGameOver = false;
    }

    public static event Action OnGameOver;

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void Pause()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void GameOver()
    {
        isGameOver = true;
        OnGameOver?.Invoke();
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public static void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}