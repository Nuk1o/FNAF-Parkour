using Cinemachine;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

public class CameraControllerPanel : MonoBehaviour
{
    private bool _isPressed = false;
    private bool _isMobile;
    private int _fingerId;
    private MobileController _mobileController;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private GoldPlayerController _goldPlayerController;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _CameraHead;
    [SerializeField] private CinemachineInputProvider _cinemachineInput;
    private float _turn;
    private Vector3 _targetHeadAngles;
    private Vector3 _targetBodyAngles;
    private void Start()
    {
        _isMobile = YandexGame.EnvironmentData.isMobile;
        if (_isMobile)
        {
            _CameraHead.SetActive(true);

            _goldPlayerController.Camera.CameraHead = null;
            _goldPlayerController.Camera.TargetVirtualCamera = null;
            _goldPlayerController.Camera.CanLookAround = false;
            _goldPlayerController.HeadBob.EnableBob = false;

            _goldPlayerController.gameObject.transform.eulerAngles = new Vector3(0, 10, 0);
            
            _targetHeadAngles = _CameraHead.transform.eulerAngles;
            _targetBodyAngles = _goldPlayerController.gameObject.transform.eulerAngles;
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.8f;
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.8f;
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
            _goldPlayerController.Camera.LookInput = "MobileLook";
        }
        else
        {
            _cinemachineVirtualCamera.gameObject.SetActive(false);
            _goldPlayerController.Camera.LookInput = "Look";
        }
    }

    private void FixedUpdate()
    {
        LookPlayer();
    }

    private void LookPlayer()
    {
        Vector2 _lookVector2 = _playerInput.actions["Look"].ReadValue<Vector2>();
        Debug.Log(_lookVector2);
        // Apply the input and mouse sensitivity.
        _targetHeadAngles.x -= _lookVector2.y * 0.8f;
        _targetBodyAngles.y += _lookVector2.x * 0.8f;
        
        _goldPlayerController.gameObject.transform.eulerAngles = _targetBodyAngles;
        _CameraHead.transform.eulerAngles = _targetHeadAngles;
        Debug.Log(_targetHeadAngles.x);
        Debug.Log(_targetBodyAngles.y);

        // Clamp the head angle.
        _targetHeadAngles.x = Mathf.Clamp(_targetHeadAngles.x, -65, 65);
        _targetBodyAngles.y = Mathf.Clamp(_targetBodyAngles.y, -90, 90);

        // Smooth the movement.
        //followHeadAngles = Vector3.SmoothDamp(followHeadAngles, targetHeadAngles, ref followHeadVelocity, lookDamping, Mathf.Infinity, Time.unscaledDeltaTime);
        //followBodyAngles = Vector3.SmoothDamp(followBodyAngles, targetBodyAngles, ref followBodyVelocity, lookDamping, Mathf.Infinity, Time.unscaledDeltaTime);
        //
        // Debug.Log("look");
        //
        // Debug.Log("look | "+_lookVector2);
        // _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = _lookVector2.x;
        // _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = _lookVector2.y;
    }

}