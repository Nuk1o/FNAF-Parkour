using UnityEngine;
using YG;
using TMPro;
using UnityEngine.UI;

public class RewardAd : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    void Rewarded(int id)
    {
        if (id == 1)
            AddEnergy();
    }
    public void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    private void AddEnergy()
    {
        if (YandexGame.savesData.energy < 5)
        {
            YandexGame.savesData.energy++;
            YandexGame.SaveProgress();
            Debug.Log("Энергия прибавлена");
        }
        else
        {
            Debug.Log("Энергия переполнена");
        }
    }
}
