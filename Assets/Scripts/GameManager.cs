using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

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
        DontDestroyOnLoad(this);
    }

}