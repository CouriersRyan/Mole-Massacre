using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    
    [SerializeField] private GameObject nestedPlayerPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.StartPoint = transform;
    }
}
