using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;

public class TimerEnergy : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyTimeTxt;
    [SerializeField] private TMP_Text _energyMin;
    private DateTime[] lastCheckTimeArr = new DateTime[5];

    private DateTime _timeNew;

    private void Awake()
    {
        _energyMin.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(YandexGame.savesData.lastLogOutTime[i]);
            lastCheckTimeArr[i]= LoadLastCheckTime($"AddEnergy{i}");
        }

        for (int j = 0; j < 5; j++)
        {
            if (lastCheckTimeArr[j]!=new DateTime())
            {
                StartCoroutine(CheckEnergy(lastCheckTimeArr[j]));
                Debug.Log($"||||||||||||||||||||||||||||||||||||||||||||||||||||TEST {j}");
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(CheckEnergy(DateTime.Now));
    }

    private void FixedUpdate()
    {
        if (YandexGame.savesData.energy<5)
        {
            if (!_energyTimeTxt.gameObject.activeInHierarchy || !_energyMin.gameObject.activeInHierarchy)
            {
                _energyMin.gameObject.SetActive(true);
                _energyTimeTxt.gameObject.SetActive(true);   
            }
        }
        else
        {
            _energyMin.gameObject.SetActive(false);
            _energyTimeTxt.gameObject.SetActive(false);   
        }
    }

    IEnumerator CheckEnergy(DateTime _dateTime)
    {
        while (true)
        {
            if (YandexGame.savesData.energy<5)
            {
                _energyTimeTxt.gameObject.SetActive(true);
                // DateTime _minutesTimer = new DateTime(YandexGame.savesData.minutesEnergy);
                // Debug.Log(_minutesTimer);
                // Debug.Log(_minutesTimer.Minute-DateTime.Now.Minute);
                // _energyTimeTxt.text = $"{_minutesTimer.Minute-DateTime.Now.Minute}";
                DateTime _minutesTimer = _dateTime;
                Debug.Log(_minutesTimer);

                TimeSpan timeDifference = _minutesTimer - DateTime.Now;

                if (timeDifference.TotalMinutes < 0)
                {
                    // Если разница отрицательная, значит, прошел новый час
                    // Добавим 60 минут к сохраненному времени
                    _minutesTimer = _minutesTimer.AddMinutes(60);
                    timeDifference = _minutesTimer - DateTime.Now;
                }

                // Используем свойство TotalMinutes для получения разницы в минутах
                int remainingMinutes = (int)timeDifference.TotalMinutes+1;

                _energyTimeTxt.text = $"{remainingMinutes}";
                if (_dateTime<DateTime.Now)
                {
                    yield break;
                }
            }
            else
            {
                _energyTimeTxt.gameObject.SetActive(false);
                _energyMin.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(10);
        }
    }
    
    private DateTime LoadLastCheckTime(string name)
    {
        switch (name)
        {
            case "AddEnergy0":
                return new DateTime(YandexGame.savesData.endOutTime[0]);
            case "AddEnergy1":
                return new DateTime(YandexGame.savesData.endOutTime[1]);
            case "AddEnergy2":
                return new DateTime(YandexGame.savesData.endOutTime[2]);
            case "AddEnergy3":
                return new DateTime(YandexGame.savesData.endOutTime[3]);
            case "AddEnergy4":
                return new DateTime(YandexGame.savesData.endOutTime[4]);
        }
        return DateTime.Now;
    }
}
