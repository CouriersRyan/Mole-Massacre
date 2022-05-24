using System.Collections.Generic;
using UnityEngine;

public class MovingLiquid : MonoBehaviour
{
    [SerializeField] private Vector2 speed;

    [SerializeField] private Vector2 force;

    private IList<Rigidbody> _contacts;

    private Material _liquid;

    private void Awake()
    {
        _liquid = GetComponent<MeshRenderer>().material;
        _contacts = new List<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _liquid.mainTextureOffset += speed * Time.fixedDeltaTime;
        foreach (var rb in _contacts) rb.velocity = force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null) _contacts.Add(collision.rigidbody);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null) _contacts.Remove(collision.rigidbody);
    }
}