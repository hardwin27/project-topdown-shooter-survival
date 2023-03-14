using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour, IHitDetector
{
    [SerializeField] private LayerMask _layerMask;

    private IHitResponder _hitResponder;

    public IHitResponder HitResponder { get { return _hitResponder; } set { _hitResponder = value; } }

    public Transform HitDetectorTransform => transform;

    private List<Collider2D> _detectedColliders = new List<Collider2D>();
    private List<Collider2D> _hittedColliders = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _detectedColliders.Add(collision);
        CheckHit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _detectedColliders.Remove(collision);
        _detectedColliders.Remove(collision);
        _hittedColliders.Remove(collision);
    }

    public void CheckHit()
    {
        HitData hitData = null;

        List<Collider2D> detectedColliders = new List<Collider2D>(_detectedColliders); 
        foreach (Collider2D detectedCollider in detectedColliders)
        {
            if (detectedCollider.TryGetComponent(out IHurtBox detectedHurtBox))
            {
                if (!_hittedColliders.Contains(detectedCollider))
                {
                    _hittedColliders.Add(detectedCollider);
                    if (detectedHurtBox.Active)
                    {
                        hitData = new HitData
                        {
                            Damage = (HitResponder == null) ? 0f : HitResponder.Damage,
                            HitPoint = detectedCollider.ClosestPoint(transform.position),
                            HurtBox = detectedHurtBox,
                            HitDetector = this,
                        };

                        if (hitData.Validate())
                        {
                            hitData.HitDetector.HitResponder?.Response(hitData);
                            hitData.HurtBox.HurtResponder?.Response(hitData);
                        }
                    }
                }
            }
        }
    }
}
