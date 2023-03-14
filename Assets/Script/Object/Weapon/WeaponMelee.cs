using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : Weapon
{
    [SerializeField] private Animator _meleeAnimator;
    [SerializeField] private WeaponAnimationEventBroadcaster _animationEventBroadcaster;
    private List<IHitDetector> _hitDetectors = new List<IHitDetector>();

    protected virtual void OnEnable()
    {
        if (_animationEventBroadcaster != null)
        {
            _animationEventBroadcaster.EventTriggered += WeaponAnimationEventTriggered;
        }
    }

    protected virtual void Start()
    {
        AssignHitbox();
    }

    protected virtual void OnDisable()
    {
        if (_animationEventBroadcaster != null)
        {
            _animationEventBroadcaster.EventTriggered -= WeaponAnimationEventTriggered;
        }
    }

    protected void AssignHitbox()
    {
        _hitDetectors = new List<IHitDetector>(GetComponentsInChildren<IHitDetector>());
        foreach (IHitDetector hitDetector in _hitDetectors)
        {
            hitDetector.HitResponder = OwnerHitResponder;
            Debug.Log($"P{gameObject.name} HITRESPONDER: {hitDetector.HitResponder.Owner.name}");
        }
    }

    protected override void Action()
    {
        base.Action();
        _meleeAnimator.SetTrigger("Attack");
    }

    public override void ActionStart()
    {
        base.ActionStart();
    }

    public override void ActionEnd()
    {
        base.ActionEnd();
    }

    protected virtual void WeaponAnimationEventTriggered(string eventName)
    {
        
    }

    protected virtual void ToggleHitDetectors(bool isEnable)
    {
        foreach (IHitDetector hitDetector in _hitDetectors)
        {
            hitDetector.HitDetectorTransform.gameObject.SetActive(isEnable);
        }
    }
}
