using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Any object that, upon facing a Rigidbody, can pick up said Rigidbody
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

    void FixedUpdate()
    {
        if (pickupPressed)
        {
            if (held)
            {
                held.transform.parent = null;
                held.useGravity = true;
                held = null;
            }
            else
            {
                var holdableMask = 1 << LayerMask.NameToLayer("Holdable");
                var holderTransform = transform;
                var holdPoint = holderTransform.position + holderTransform.TransformPoint(holdOffset);
                var facingRay = new Ray(holdPoint, holderTransform.forward);
                if (Physics.Raycast(facingRay, out var hitInfo, maxDistance: Mathf.Infinity, layerMask: holdableMask))
                {
                    held = hitInfo.rigidbody;
                    held.transform.position = holdPoint;
                    held.transform.parent = holderTransform;
                    held.useGravity = false;
                }
            }
        }

        pickupPressed = false;
    }

    void OnPickUp(InputValue inputValue)
    {
        pickupPressed = inputValue.isPressed;
    }
}