using System.Collections.Generic;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class DamageDealChainEffect : DamageDealEffectComponent
{
    [SerializeField, Required]
    private FloatVariable radius;

    [SerializeField, Required]
    private FloatVariable damage;

    [SerializeField, ReadOnly]
    private List<DamageReceiver> detectedReceivers = new List<DamageReceiver>(10);

    [SerializeField, Required]
    private Tag damageableTag;

    [SerializeField, Required]
    private LineRenderer lineLight;
    
    [SerializeField]
    private DamageType damageType;

    private readonly List<LineRenderer> lineRenderers = new List<LineRenderer>(10);

    private readonly Collider[] colliders = new Collider[100];

    public override void ApplyEffect(DamageReceiver receiver, float damage)
    {
        this.detectedReceivers.Clear();
        ClearVisualEffect();
        if (!receiver.ContainsTag(this.damageableTag)) return;
        this.detectedReceivers.Add(receiver);
        DetectReceiver(receiver);
        if (this.detectedReceivers.IsEmpty()) return;
        ApplyVisualEffect();
        ApplyDamage();
    }

    private void DetectReceiver(DamageReceiver source)
    {
        this.colliders.CleanUp();
        int numberCols = Physics.OverlapSphereNonAlloc(
            source.transform.position,
            this.radius.Value,
            this.colliders);
        for (int i = 0; i < numberCols; i++)
        {
            if (this.colliders[i] == null) continue;
            DamageReceiver receiver = this.colliders[i].GetComponent<DamageReceiver>();
            if (receiver == null) continue;
            if (this.detectedReceivers.Contains(receiver)) continue;
            if (!receiver.ContainsTag(this.damageableTag)) continue;
            this.detectedReceivers.Add(receiver);
            DetectReceiver(receiver);
        }
    }

    private void ApplyDamage()
    {
        foreach (DamageReceiver receiver in this.detectedReceivers)
        {
            receiver.TakeDamage(this.damage.Value, this.damageType);
        }
    }

    private void ApplyVisualEffect()
    {
        for (int i = 0; i < this.detectedReceivers.Count - 1; i++)
        {
            LineRenderer lineRenderer = Instantiate(this.lineLight);
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, 
                this.detectedReceivers[i].transform.parent.position.ReplaceY(1));
            lineRenderer.SetPosition(1,
                this.detectedReceivers[i + 1].transform.parent.position.ReplaceY(1));
            this.lineRenderers.Add(lineRenderer);
        }
    }

    private void ClearVisualEffect()
    {
        for (int i = this.lineRenderers.Count - 1; i >= 0; i--)
        {
            Destroy(this.lineRenderers[i].gameObject);
        }

        this.lineRenderers.Clear();
    }
}