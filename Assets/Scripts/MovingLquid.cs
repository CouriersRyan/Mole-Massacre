using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLquid : MonoBehaviour
{

    [SerializeField] private Vector2 speed;

    [SerializeField] private Vector2 force;

    private Material _liquid;

    private List<Rigidbody> _contacts;

    // Start is called before the first frame update
    void Start()
    {
        _liquid = GetComponent<MeshRenderer>().material;
        _contacts = new List<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _liquid.mainTextureOffset += speed / 60f;
        foreach (var rb in _contacts)
        {
            rb.velocity = force;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            _contacts.Add(collision.rigidbody);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            _contacts.Remove(collision.rigidbody);
        }
    }
}
