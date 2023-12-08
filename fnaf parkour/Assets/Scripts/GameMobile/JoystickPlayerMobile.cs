using Cinemachine;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickPlayerMobile : MonoBehaviour
{
    [SerializeField] private GoldPlayerController _goldPlayerController;
    [SerializeField] private Transform _goldPlayerTransform;
    [SerializeField] private PlayerInput _playerInput;
    //[SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private Vector3 moveDirection;

    private void Update()
    {
        MovePlayer();
    }
    
    private void MovePlayer()
    {
        //Debug.Log("Walk");
        Vector2 move = _playerInput.actions["Move"].ReadValue<Vector2>();
        Debug.Log("Walk | " + move);
        //Vector3 direction = (_goldPlayerController.Camera.TargetVirtualCamera.transform.forward * move.y + _goldPlayerController.Camera.TargetVirtualCamera.transform.right * move.x);
        Vector3 move1 = new Vector3(move.x, 0, move.y);
        move1 = move1.x * _goldPlayerTransform.right + move1.z * _goldPlayerTransform.forward;
        move1.y = 0f;
        
        // moveDirection = new Vector3(move.x, moveDirection.y, move.y);
        // moveDirection = Quaternion.LookRotation(_goldPlayerController.Camera.BodyForward, _goldPlayerController.gameObject.transform.up) * moveDirection;
        // Debug.Log(moveDirection);
        // Debug.Log(_goldPlayerTransform.right);
        // Debug.Log(_goldPlayerTransform.forward);
        // _goldPlayerController.Controller.Move(moveDirection * Time.deltaTime * _goldPlayerController.Movement.WalkingSpeeds.ForwardSpeed);
        _goldPlayerController.Controller.Move(move1*Time.deltaTime*_goldPlayerController.Movement.WalkingSpeeds.ForwardSpeed);
    }
}
