using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileDamage;
    [SerializeField] private Rigidbody2D _body;

    private IHitDetector _hitBox;

    public Rigidbody2D Body { get { return _body; } }
    public IHitDetector HitBox { get { return _hitBox; } }
    /*public IHitResponder OwnerHitResponder { set; get; }*/

    private void OnEnable()
    {
        _hitBox = GetComponent<IHitDetector>();
    }

    public float ProjectileDamage
    {
        set { _projectileDamage = value; }
        get { return _projectileDamage; }
    }

    public float Damage => _projectileDamage;
}
