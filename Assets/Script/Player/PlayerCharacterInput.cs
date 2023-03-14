using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInput : MonoBehaviour
{
    [SerializeField] private CharacterBaseController _characterBaseController;

    private void Update()
    {
        SetAxisInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        SetMousePositionInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(0))
        {
            MouseInputDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MouseInputUp();
        }
    }

    private void SetAxisInput(Vector2 axisInput)
    {
        _characterBaseController.Move(axisInput);
    }

    private void SetMousePositionInput(Vector2 mousePos)
    {
        _characterBaseController.LookTo(mousePos);
    }

    private void MouseInputDown()
    {
        _characterBaseController.StartCombatAction();
    }

    private void MouseInputUp()
    {
        _characterBaseController.EndCombatAction();
    }
}
