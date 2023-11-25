using Hertzole.GoldPlayer;
using UnityEngine;
using YG;

public class LoadingSettings : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private GoldPlayerController _goldPlayerController;
    
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
        _goldPlayerController.Camera.MouseSensitivity = new Vector2(_sens,_sens);
        foreach (var audio in _audioSources)
        {
            audio.volume = _vol;
        }
    }
}
