using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuYG : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;

    [Space]
    [Header("Settings")]
    [SerializeField] private Slider _volume;
    [SerializeField] private Slider _sensivity;
    public void AuthOn()
    {
        Debug.Log("Авторизация успешна");
        Debug.Log(YandexGame.playerName);
        Debug.Log(YandexGame.playerPhoto);
        if (YG.YandexGame.auth)
        {
            _name.text = YandexGame.playerName;
        }
    }
    
    public void AuthOff()
    {
        Debug.Log("Авторизация не выплонена");
        if (!YG.YandexGame.auth)
        {
            _name.text = "Пользователь";
        }
    }

    public void SaveSettings()
    {
        YandexGame.savesData.sensivity = _sensivity.value;
        YandexGame.savesData.volumeAudio = _volume.value;
        YandexGame.SaveProgress();
    }
}
