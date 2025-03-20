using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Condition.Core;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class TagDetectorCondition : Condition
{
    private bool isDetected;

    [SerializeField, Required]
    private Tag detectedTag;

    [SerializeField, ReadOnly]
    private Collider lastCollider;

    [SerializeField, ReadOnly]
    private Tags lastTags;

    public override bool IsMet => this.isDetected;

    protected override async UniTask OnEntered()
    {
        await base.OnEntered();
        this.isDetected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Detect(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Detect(other);
    }

    private void Detect(Collider other)
    {
        if (this.lastCollider == other)
        {
            if (this.lastTags.GetTags().Contains(this.detectedTag))
            {
                this.isDetected = true;
                return;
            }
        }

        Tags tags = other.GetComponent<Tags>();
        if (tags == null) return;
        if (!tags.GetTags().Contains(this.detectedTag)) return;
        this.lastTags = tags;
        this.lastCollider = other;
        this.isDetected = true;
    }

    [Button]
    private void Rename()
    {
        gameObject.name = $"Detect [{this.detectedTag.name}] tag";
    }
}