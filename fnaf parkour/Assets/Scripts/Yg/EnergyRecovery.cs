using System;
using System.Collections;
using UnityEngine;
using YG;

public class EnergyRecovery : MonoBehaviour
{
    private int _minutesHold = 15;
    private DateTime _dateTimeEnergy;
    public static EnergyRecovery instance;
    public static GameObject energyRecoveryScript;
    
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Awake()
    {
        energyRecoveryScript = GameObject.Find("energyRecoveryScript");
 
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(energyRecoveryScript);
        }
    }
    
    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }
    public void GetLoad()
    {
        StartCoroutine(CheckEnergy());
    }

    IEnumerator CheckEnergy()
    {
        while (true)
        {
            if (YandexGame.savesData.energy < 5 && !YandexGame.savesData.isPlay)
            {
                EnergyRec();
            }
            yield return new WaitForSeconds(5);
        }
    }
    private void EnergyRec()
    {
        if (YandexGame.savesData.dateTicks == 0)
        {
            Debug.Log("Тики: "+YandexGame.savesData.dateTicks);
            YandexGame.savesData.dateTicks = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,
                DateTime.Now.Hour,DateTime.Now.Minute,0).AddMinutes(_minutesHold).Ticks;
            Debug.Log("Новые тики: "+YandexGame.savesData.dateTicks);
            YandexGame.SaveProgress();
            Debug.Log("Пошло начисление энергии");
        }
        else
        {
            Debug.Log("Время: "+ new DateTime(YandexGame.savesData.dateTicks));
            Debug.Log("Время сейчас: "+new DateTime(DateTime.Now.Ticks));
            if (DateTime.Now.Ticks>=YandexGame.savesData.dateTicks && YandexGame.savesData.energy < 5)
            {
                int _energy = YandexGame.savesData.energy;
                _energy++;
                YandexGame.savesData.energy = _energy;
                YandexGame.savesData.dateTicks = 0;
                YandexGame.SaveProgress();
                Debug.Log("Энергия прибавлена");
            }
        }
        
    }
}
