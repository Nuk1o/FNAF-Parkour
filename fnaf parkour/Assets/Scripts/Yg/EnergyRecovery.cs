using System;
using System.Collections;
using UnityEngine;
using YG;

public class EnergyRecovery : MonoBehaviour
{
    private int _h, _m;
    private DateTime _dateTimeEnergy;

    public static EnergyRecovery instance;
    public static GameObject energyRecoveryScript;

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
        StartCoroutine(CheckEnergy());
    }

    IEnumerator CheckEnergy()
    {
        while (true)
        {
            if (YandexGame.savesData.energy < 5)
            {
                EnergyRec();
            }
            yield return new WaitForSeconds(60);
        }
    }

    private void EnergyRec()
    {
        Debug.Log(YandexGame.savesData.dateTimes);
        if (YandexGame.savesData.dateTimes == new DateTime())
        {
            _h = DateTime.Now.Hour;
            _m = DateTime.Now.Minute;
            _m = _m + 1;
            if (_m>= 60)
            {
                _m = _m - 60;
                _h++;
            }
            if (_h>=24)
            {
                _h = 0;
            }
            _dateTimeEnergy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, _h, _m, 0);
            YandexGame.savesData.dateTimes = _dateTimeEnergy;
            YandexGame.SaveProgress();
            Debug.Log(YandexGame.savesData.dateTimes);
            Debug.Log("Пошло начисление энергии");
        }
        else
        {
            if (DateTime.Now >= YandexGame.savesData.dateTimes)
            {
                int _energy = YandexGame.savesData.energy;
                _energy++;
                YandexGame.savesData.energy = _energy;
                YandexGame.savesData.dateTimes = new DateTime();
                Debug.Log(YandexGame.savesData.dateTimes);
                YandexGame.SaveProgress();
                Debug.Log("Энергия прибавлена");
            }
        }
    }
}
