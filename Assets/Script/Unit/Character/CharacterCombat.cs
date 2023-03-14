using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour, IAttackStats, IHitResponder
{
    [SerializeField] private float _attackPower;
    [SerializeField] private Weapon _weapon;

    public float AttackPower
    {
        set { _attackPower = value; }
        get { return _attackPower; }
    }

    public float Damage => AttackPower;

    public GameObject Owner => gameObject;

    private void OnEnable()
    {
        _weapon.OwnerHitResponder = this;
    }

    public void StartAction()
    {
        _weapon.ActionStart();
    }

    public void EndAction()
    {
        _weapon.ActionEnd();
    }

    public bool CheckHit(HitData hitData)
    {
        return (hitData.HurtBox.HurtBoxTransform.gameObject != gameObject);
    }

    public void Response(HitData hitData)
    {
        Debug.Log($"{gameObject.name} RESPONSE TO HIT");
    }
}
