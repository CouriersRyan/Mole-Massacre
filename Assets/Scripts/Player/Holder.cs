using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Any object that, upon facing a Fixed Joint, can pick up said Fixed Joint.
///     The Fixed Joint must be in the "Holdable" layer.
/// </summary>
public class Holder : MonoBehaviour
{
    /// <summary>
    ///     The FixedJoint we are currently holding. Null if nothing is held.
    /// </summary>
    private FixedJoint held;

    /// <summary>
    ///     What the Spring Joint was attached to before the player held it.
    /// </summary>
    private Rigidbody oldConnectedPoint;

    [SerializeField] private float range;

    private Rigidbody rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    ///     Called by Unity's new input system
    ///     Lets go of held item when held, reenabling physics on rigidbodies
    ///     Holds any rigidbody in the holdable layers that the Holder is facing if no item is held
    ///     Holding a rigidbody makes it kinematic.
    /// </summary>
    /// <param name="inputValue">Ignored.</param>
    void OnPickUp(InputValue inputValue)
    {
        if (held)
        {
            foreach (Transform child in held.GetComponentsInChildren<Transform>())
                child.gameObject.layer = LayerMask.NameToLayer("Holdable");

            held.gameObject.layer = LayerMask.NameToLayer("Holdable");
            held.connectedBody = oldConnectedPoint;    // TODO: letting go of corpse while a force is being applied causes it to fly away
            held = null;
        }
        else
        {
            var facingRay = new Ray(transform.position, transform.forward);
            var holdableLayer = LayerMask.GetMask("Holdable");
            if (Physics.Raycast(facingRay, out var hitInfo, maxDistance: range, layerMask: holdableLayer))
            {
                var hitObj = hitInfo.transform.root;
                held = hitObj.GetComponent<FixedJoint>();
                oldConnectedPoint = held.connectedBody;
                held.connectedBody = rbody;
                foreach (Transform child in held.GetComponentsInChildren<Transform>())
                    child.gameObject.layer = LayerMask.NameToLayer("Held");

                held.gameObject.layer = LayerMask.NameToLayer("Held");
            }
        }
    }
}