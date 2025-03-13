using System;
using System.Collections;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField, Required]
    private Material targetMaterial;
    
    [SerializeField, Required]
    private Renderer ren;
    
    [SerializeField, Required]
    private float duration;
    
    [SerializeField, ReadOnly]
    private Material originalMaterial;

    public void ChangeMaterial()
    {
        this.originalMaterial = this.ren.material;
        StopCoroutine(Change());
        StartCoroutine(Change());
    }

    private IEnumerator Change()
    {
        this.ren.material = this.targetMaterial;
        yield return new WaitForSeconds(this.duration);
        this.ren.material = this.originalMaterial;
    }
}