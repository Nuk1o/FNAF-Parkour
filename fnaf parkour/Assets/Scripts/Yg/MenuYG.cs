using TMPro;
using UnityEngine;
using YG;

public class MenuYG : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
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
}
