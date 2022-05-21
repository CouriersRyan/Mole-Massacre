using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Any object that, upon facing a Rigidbody, can pick up said Rigidbody
///     Rigidbodies need to be in the "Holdable" layer to be held.
/// </summary>
public class Holder : MonoBehaviour
{
    /// <summary>
    ///     Distance between position and holdable when held
    /// </summary>
    [SerializeField] private Vector3 holdOffset;

    /// <summary>
    ///     The rigidbody we are currently holding. Null if nothing is held.
    /// </summary>
    private Rigidbody held;

    /// <summary>
    ///     bruh
    /// </summary>
    private bool pickupPressed;

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (pickupPressed)
        {
            if (held)
            {
                held.transform.parent = null;
                held.useGravity = true;
                held.isKinematic = false;
                held = null;
            }
            else
            {
                var holdableMask = 1 << LayerMask.NameToLayer("Holdable");
                var facingRay = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(facingRay, out var hitInfo, maxDistance: Mathf.Infinity, layerMask: holdableMask))
                {
                    held = hitInfo.rigidbody;
                    held.transform.position = transform.position + transform.TransformVector(holdOffset);
                    held.transform.parent = transform;
                    held.isKinematic = true;
                    held.useGravity = false;
                }
            }
        }

        pickupPressed = false;
    }

    private void OnDrawGizmos()
    {
        var holdPoint =
            transform.position;

        Debug.DrawRay(holdPoint, 10f * transform.forward, Color.green);
    }

    void OnPickUp(InputValue inputValue)
    {
        pickupPressed = inputValue.isPressed;
    }
}