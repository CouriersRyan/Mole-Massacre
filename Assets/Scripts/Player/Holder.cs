using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Any object that, upon facing a gameobject, can pick up said gameobject.
///     Gameobjects need to be in the "Holdable" layer to be held.
/// </summary>
public class Holder : MonoBehaviour
{
    /// <summary>
    ///     Distance between position and holdable when held
    /// </summary>
    [SerializeField] private Vector3 holdOffset;

    /// <summary>
    ///     The Gameobject we are currently holding. Null if nothing is held.
    /// </summary>
    private Transform held;

    /// <summary>
    ///     bruh
    /// </summary>
    private bool pickupPressed;

    void FixedUpdate()
    {
        if (pickupPressed)
        {
            if (held)
            {
                held.transform.parent = null;
                foreach (var childRBody in held.GetComponentsInChildren<Rigidbody>())
                {
                    childRBody.useGravity = true;
                    childRBody.isKinematic = false;
                }

                held = null;
            }
            else
            {
                var holdableMask = 1 << LayerMask.NameToLayer("Holdable");
                var facingRay = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(facingRay, out var hitInfo, maxDistance: Mathf.Infinity, layerMask: holdableMask))
                {
                    held = hitInfo.transform.root;
                    foreach (var childRBody in held.GetComponentsInChildren<Rigidbody>())
                    {
                        childRBody.useGravity = false;
                        childRBody.isKinematic = true;
                    }
                    held.transform.position = transform.position + transform.TransformVector(holdOffset);
                    held.transform.parent = transform;
                }
            }
        }

        pickupPressed = false;
    }

    /// <summary>
    ///     Called by Unity's new input system
    /// </summary>
    /// <param name="inputValue">Input values passed in by input system.</param>
    void OnPickUp(InputValue inputValue)
    {
        pickupPressed = inputValue.isPressed;
    }
}