using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected bool _isOnAction = false;
    [SerializeField] protected float _actionInterval;
    [SerializeField] protected float _actionIntervalTimer;
    protected IHitResponder _ownerHitResponder;

    public IHitResponder OwnerHitResponder { set; get; }

    protected virtual void Update()
    {
        AutoActtion();
        ActionIntervalTimerHandler();
    }

    protected virtual void AutoActtion()
    {
        if (_isOnAction)
        {
            if (_actionIntervalTimer > 0f)
            {
                return;
            }
            _actionIntervalTimer = _actionInterval;
            Action();
        }
    }

    protected virtual void ActionIntervalTimerHandler()
    {
        if (_actionIntervalTimer > 0f)
        {
            _actionIntervalTimer -= Time.deltaTime;
        }
    }

    protected virtual void Action()
    {

    }

    
    
    public virtual void ActionStart()
    {
        _isOnAction = true;
    }

    public virtual void ActionEnd()
    {
        _isOnAction = false;
    }
}
