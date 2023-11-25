using UnityEngine;
using YG;
public class DeathVolume : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    private float _vol;
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
        SetVolumeDeath();
    }

    public void GetData()
    {
        _vol = YandexGame.savesData.volumeAudio;
    }

    private void SetVolumeDeath()
    {
        _audio.volume = _vol;
    }
}
