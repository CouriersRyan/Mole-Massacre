using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public static List<Explosive> Explosives;
    [SerializeField] private float power;
    [SerializeField] private float powerPlayer;

    /// <summary>
    ///     The layers which this explosion should affect.
    /// </summary>
    [SerializeField] private LayerMask layerMask;

    private Rigidbody[] _bodyParts;
    private CharacterJoint[] _joints;

    private void Awake()
    {
        _bodyParts = GetComponentsInChildren<Rigidbody>();
        _joints = GetComponentsInChildren<CharacterJoint>();
        if (Explosives == null) Explosives = new List<Explosive>();
        Explosives.Add(this);
    }

    public void Explode()
    {
        foreach (var joint in _joints) Destroy(joint);

        foreach (var part in _bodyParts)
            part.AddForce((part.transform.position - transform.position).normalized * power, ForceMode.Force);

        var hits = Physics.OverlapSphere(transform.position, 20f, layerMask);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Corpse"))
                hit.attachedRigidbody.AddForce((hit.transform.position - transform.position).normalized * power,
                    ForceMode.Impulse);

            if (hit.CompareTag("Player"))
            {
                var controller = hit.GetComponent<ThirdPersonController>();
                controller.SetKnockback(hit.transform.position - transform.position, powerPlayer);
            }
        }

        Explosives.Remove(this);
    }
}