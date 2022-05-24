using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    private Rigidbody[] _bodyParts;
    private CharacterJoint[] _joints;

    [SerializeField] private float power;
    [SerializeField] private LayerMask layerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        _bodyParts = GetComponentsInChildren<Rigidbody>();
        _joints = GetComponentsInChildren<CharacterJoint>();
        Explode();
    }

    public void Explode()
    {
        foreach (var joint in _joints)
        {
            Destroy(joint);
        }
        foreach (var part in _bodyParts)
        {
            part.AddForce((part.transform.position - transform.position).normalized * power, ForceMode.Force);
        }

        var hits = Physics.OverlapSphere(transform.position, 20f, layerMask);
        foreach (var hit in hits)
        {
            if(hit.attachedRigidbody != null) hit.attachedRigidbody.AddForce((hit.transform.position - transform.position).normalized * power, ForceMode.Impulse);
        }
    }
}
