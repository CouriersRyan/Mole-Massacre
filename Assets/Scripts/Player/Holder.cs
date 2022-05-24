using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Any object that, upon facing a Joint, can pick up said Joint.
///     The Joint must be in the "Holdable" layer.
/// </summary>
public class Holder : MonoBehaviour
{
    [SerializeField] private float range;

    /// <summary>
    ///     The Joint we are currently holding. Null if nothing is held.
    /// </summary>
    private Joint held;

    /// <summary>
    ///     What the Joint was attached to before the player held it.
    /// </summary>
    private Rigidbody oldConnectedPoint;

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
    private void OnPickUp(InputValue inputValue)
    {
        if (held)
        {
            foreach (var child in held.GetComponentsInChildren<Transform>())
                child.gameObject.layer = LayerMask.NameToLayer("Holdable");

            held.gameObject.layer = LayerMask.NameToLayer("Holdable");
            held.connectedBody = oldConnectedPoint;
            held = null;
        }
        else
        {
            var facingRay = new Ray(transform.position, transform.forward);
            var holdableLayer = LayerMask.GetMask("Holdable");
            if (Physics.Raycast(facingRay, out var hitInfo, range, holdableLayer))
            {
                var hitObj = hitInfo.transform.root;
                held = hitObj.GetComponent<Joint>();
                oldConnectedPoint = held.connectedBody;
                held.connectedBody = rbody;
                foreach (var child in held.GetComponentsInChildren<Transform>())
                    child.gameObject.layer = LayerMask.NameToLayer("Held");

                held.gameObject.layer = LayerMask.NameToLayer("Held");
            }
        }
    }
}