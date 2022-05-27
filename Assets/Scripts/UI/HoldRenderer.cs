using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chooses whether or not to render the outline depending on if the object is being held.
public class HoldRenderer : MonoBehaviour
{
    private SkinnedMeshRenderer _renderer;
    [SerializeField] private Material outline;
    private static readonly int OutlineToggle = Shader.PropertyToID("_OutlineToggle");

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SkinnedMeshRenderer>();
        _renderer.materials[1].enableInstancing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Holdable"))
        {
            _renderer.materials[1].SetFloat(OutlineToggle, 0);
        }

        if (gameObject.layer == LayerMask.NameToLayer("Held"))
        {
            _renderer.materials[1].SetFloat(OutlineToggle, 1);
        }
    }
}
