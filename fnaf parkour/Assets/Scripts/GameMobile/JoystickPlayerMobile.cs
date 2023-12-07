using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class JoystickPlayerMobile : MonoBehaviour
{
    [SerializeField] private GoldPlayerController _goldPlayerController;
    [SerializeField] private PlayerInput _playerInput;

    private void Update()
    {
        MovePlayer();
    }
    
    private void MovePlayer()
    {
        if (_playerInput.actions["Move"].triggered)
        {
            Debug.Log("Walk");
            Vector2 move = _playerInput.actions["Move"].ReadValue<Vector2>();
            Debug.Log("Walk | " + move);
            Vector3 direction = (Vector3.forward * move.y + Vector3.right * move.x)*Time.deltaTime*_goldPlayerController.Movement.WalkingSpeeds.ForwardSpeed;
            _goldPlayerController.Controller.Move(direction);
        }
    }
}
