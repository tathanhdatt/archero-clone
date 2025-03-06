using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class ScrollSkillView : BaseView
{
    [SerializeField]
    private Animator animator;

    [SerializeField, ValueDropdown(nameof(GetAnimNames))]
    private string startScrollAnimName;

    private string[] GetAnimNames()
    {
        return this.animator.GetAnimationNames();
    }
}