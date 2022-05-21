using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
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
    }

    private Transform _startPoint;

    public Transform StartPoint
    {
        get
        {
            if (_startPoint != null)
            {
                return _startPoint;
            }
            return null;
        }

        set
        {
            _startPoint = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public delegate void StartLevel();
    public StartLevel StartLevelEvent;
}
