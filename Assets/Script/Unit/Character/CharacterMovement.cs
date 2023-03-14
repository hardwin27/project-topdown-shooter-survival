using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;

    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _lookDirectionInput = Vector2.zero;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _accelerationRate;
    [SerializeField] private float _deaccelerationRate;
    [SerializeField] private float _velocityPowerScale;
    [SerializeField] private float _frictionAmount;

    private void FixedUpdate()
    {
        LookDirectionHandler();
        MoveHandler();
        FrictionHandler();
    }

    public void InputMove(Vector2 moveInput)
    {
        _moveInput = moveInput;
    }

    public void InputLookTo(Vector2 lookTo)
    {

        _lookDirectionInput = lookTo - _body.position;
    }

    private void LookDirectionHandler()
    {
        if (_lookDirectionInput == Vector2.zero)
        {
            return;
        }

        float lookAngle = Mathf.Atan2(_lookDirectionInput.y, _lookDirectionInput.x) * Mathf.Rad2Deg;
        _body.rotation = lookAngle;
    }

    private void MoveHandler()
    {
        Vector2 moveInput = _moveInput.normalized;
        float targetSpeedX = moveInput.x * _moveSpeed;
        float speedDifX = targetSpeedX - _body.velocity.x;
        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? _accelerationRate : _deaccelerationRate;
        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, _velocityPowerScale) * Mathf.Sign(speedDifX);
        movementX = Mathf.Round(movementX * 100f / 100f);

        float targetSpeedY = moveInput.y * _moveSpeed;
        float speedDifY = targetSpeedY - _body.velocity.y;
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? _accelerationRate : _deaccelerationRate;
        float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, _velocityPowerScale) * Mathf.Sign(speedDifY);
        movementY = Mathf.Round(movementY * 100f / 100f);

        _body.AddForce(new Vector2(movementX, movementY));
    }

    private void FrictionHandler()
    {
        float xFrictionAmount = 0f;
        float yFrictionAmount = 0f;
        if (Mathf.Abs(_moveInput.x) < 0.1f)
        {
            xFrictionAmount = Mathf.Min(Mathf.Abs(_body.velocity.x), Mathf.Abs(_frictionAmount));
            xFrictionAmount *= Mathf.Sign(_body.velocity.x);
        }
        if (Mathf.Abs(_moveInput.y) < 0.1f)
        {
            yFrictionAmount = Mathf.Min(Mathf.Abs(_body.velocity.y), Mathf.Abs(_frictionAmount));
            yFrictionAmount *= Mathf.Sign(_body.velocity.y);
        }
        Vector2 friction = new Vector2(xFrictionAmount, yFrictionAmount);
        _body.AddForce(-friction, ForceMode2D.Impulse);
    }
}
