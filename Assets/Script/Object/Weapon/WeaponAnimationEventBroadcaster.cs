using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEventBroadcaster : MonoBehaviour
{
    public delegate void WeaponAnimationEvent(string eventName);
    public event WeaponAnimationEvent EventTriggered;

    public void TriggerEvent(string eventName)
    {
        EventTriggered?.Invoke(eventName);
    }
}
