using UnityEngine;
using UnityEngine.UI;
using YG;

public class LoadSettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _audio;
    [SerializeField] private Slider _sensivity;
    
    private float _sens, _vol;
    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    private void Start()
    {
        SetSettings();
    }

    public void GetData()
    {
        _sens = YandexGame.savesData.sensivity;
        _vol = YandexGame.savesData.volumeAudio;
    }

    private void SetSettings()
    {
        _audio.value = _vol;
        _sensivity.value = _sens;
    }
}
