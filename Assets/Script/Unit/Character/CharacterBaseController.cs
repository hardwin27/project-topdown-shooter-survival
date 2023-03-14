using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseController : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterCombat _characterCombat;

    public void Move(Vector2 moveDirection)
    {
        _characterMovement.InputMove(moveDirection);
    }

    public void LookTo(Vector2 positionToLook)
    {
        _characterMovement.InputLookTo(positionToLook);
    }

    public void StartCombatAction()
    {
        _characterCombat.StartAction();
    }

    public void EndCombatAction()
    {
        _characterCombat.EndAction();
    }
}
