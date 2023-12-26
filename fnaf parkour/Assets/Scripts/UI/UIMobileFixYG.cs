using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIMobileFixYG : MonoBehaviour
{
    [SerializeField] private CanvasScaler _canvasScaler;

    private void Awake()
    {
        if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.deviceType == "mobile" || Application.isMobilePlatform)
        {
            _canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }
        else
        {
            _canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        }
    }
}
