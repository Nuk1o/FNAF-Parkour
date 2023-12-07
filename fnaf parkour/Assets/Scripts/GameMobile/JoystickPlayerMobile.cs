using Hertzole.GoldPlayer;
using UnityEngine;

public class JoystickPlayerMobile : MonoBehaviour
{
    [SerializeField] private VariableJoystick _variableJoystick;
    [SerializeField] private GoldPlayerController _goldPlayerController;
    
    private void FixedUpdate()
    {
        Vector3 direction = (Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal)*Time.deltaTime*_goldPlayerController.Movement.WalkingSpeeds.ForwardSpeed;
        _goldPlayerController.Controller.Move(direction);
    }
}
