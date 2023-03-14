using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour, IHealthStats, IHurtResponder
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    public float CurrentHealth 
    {
        set { _currentHealth = value; }
        get { return _currentHealth; } 
    }
    public float MaxHealth
    {
        set { _maxHealth = value; }
        get { return _maxHealth; }
    }

    public GameObject Owner => gameObject;

    private List<IHurtBox> _hurtBoxes;

    private void OnEnable()
    {
        _hurtBoxes = new List<IHurtBox>(GetComponentsInChildren<IHurtBox>());
        foreach (IHurtBox hurtbox in _hurtBoxes)
        {
            hurtbox.HurtResponder = this;
        }    

    }

    public void AddHealth(float addedValue)
    {
        Debug.Log($"{gameObject.name} Health AddedValue: {addedValue}");
        CurrentHealth = Mathf.Clamp(CurrentHealth + addedValue, 0f, MaxHealth);
        CheckIsDeath();
    }

    public bool CheckHit(HitData hitData)
    {
        return true;
    }

    public void Response(HitData hitData)
    {
        Debug.Log($"{gameObject.name} RESPONSE TO HURT");
        AddHealth(-hitData.Damage);
    }

    private  void CheckIsDeath()
    {
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
