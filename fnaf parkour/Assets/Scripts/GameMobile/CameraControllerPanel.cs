using Cinemachine;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;
using UnityEngine.EventSystems;

public class CameraControllerPanel : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    private bool _isPressed = false;
    private bool _isMobile;
    private int _fingerId;
    private MobileController _mobileController;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private GoldPlayerController _goldPlayerController;
    private void Start()
    {
        _isMobile = YandexGame.EnvironmentData.isMobile;
        if (_isMobile)
        {
            _cinemachineVirtualCamera.gameObject.SetActive(true);
            _goldPlayerController.Camera.LookInput = "MobileLook";
            _mobileController = new MobileController();
            _mobileController.MobileCameraController.Enable();
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.8f;
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.8f;
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
        }
        else
        {
            _cinemachineVirtualCamera.gameObject.SetActive(false);
            _goldPlayerController.Camera.LookInput = "Look";
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        
        if(eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            _isPressed = true;
            _mobileController.MobileCameraController.LookMobile.performed += PlayerLook;
        }
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        _mobileController.MobileCameraController.LookMobile.performed -= PlayerLook;
        _isPressed = false;
        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = 0;
        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = 0;
    }

    private void PlayerLook(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Debug.Log(context.action);
        Debug.Log(context.control);
        Debug.Log(context.ReadValue<float>());
        if (_mobileController.MobileCameraController.LookMobile.activeControl.name == "x")
        {
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = context.ReadValue<float>();
        }
        else if(_mobileController.MobileCameraController.LookMobile.activeControl.name == "y")
        {
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = context.ReadValue<float>();
        }
    }
}