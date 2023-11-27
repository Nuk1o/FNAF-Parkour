using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuYG : MonoBehaviour
{
    [Space]
    [Header("Settings")]
    [SerializeField] private Slider _volume;
    [SerializeField] private Slider _sensivity;
    public void SaveSettings()
    {
        YandexGame.savesData.sensivity = _sensivity.value;
        YandexGame.savesData.volumeAudio = _volume.value;
        YandexGame.SaveProgress();
    }
}
