using UnityEngine;
using UnityEngine.UI;
using YG;

public class MobileUI : MonoBehaviour
{
    [SerializeField] private CanvasScaler _canvasScaler;
    [SerializeField] private RectTransform _bgUI;
    [SerializeField] private bool _isFirstWindow;
    private int _sizeW, _sizeH;
    void Start()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            if (_isFirstWindow)
            {
                Debug.Log("Screen Width: " + Screen.width);
                Debug.Log("Screen Height: " + Screen.height);
                _canvasScaler.referenceResolution = new Vector2(Screen.width,Screen.height);
                _bgUI.sizeDelta = new Vector2(Screen.width, Screen.height);
                YandexGame.savesData.widhtScreen = Screen.width;
                YandexGame.savesData.heightScreen = Screen.height;
                YandexGame.SaveProgress();
                _isFirstWindow = false;
            }
            else
            {
                _sizeW = YandexGame.savesData.widhtScreen;
                _sizeH = YandexGame.savesData.heightScreen;
                _canvasScaler.referenceResolution = new Vector2(_sizeW,_sizeH);
                _bgUI.sizeDelta = new Vector2(_sizeW, _sizeH);
            }
        }
        else
        {
            _canvasScaler.referenceResolution = new Vector2(1920,1080);
            _bgUI.sizeDelta = new Vector2(1920, 1080);
        }
    }
}
