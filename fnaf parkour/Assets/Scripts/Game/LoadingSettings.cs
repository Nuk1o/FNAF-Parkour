using Hertzole.GoldPlayer;
using UnityEngine;
using YG;

public class LoadingSettings : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private GoldPlayerController _goldPlayerController;
    [SerializeField] private AudioClip[] _audioClips;
    
    private float _sens, _vol;
    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
            SetSettings();
        }
    }

    public void GetData()
    {
        _sens = YandexGame.savesData.sensivity;
        _vol = YandexGame.savesData.volumeAudio;
    }

    private void SetSettings()
    {
        Debug.Log(_sens);
        _goldPlayerController.Camera.MouseSensitivity = new Vector2(_sens,_sens);
        foreach (var audio in _audioSources)
        {
            audio.volume = _vol;
        }
        AudioItem volumeAudioItem = new AudioItem(true, true, 1f, 0.9f, 1.1f, true, _vol,_audioClips);
        _goldPlayerController.Audio.WalkFootsteps = volumeAudioItem;
        _goldPlayerController.Audio.CrouchFootsteps = volumeAudioItem;
        _goldPlayerController.Audio.RunFootsteps = volumeAudioItem;
        
    }
}
