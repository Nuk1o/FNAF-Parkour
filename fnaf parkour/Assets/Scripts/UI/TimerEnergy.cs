using System;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class TimerEnergy : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyTimeTxt;
    [SerializeField] private TMP_Text _energyMin;
    private EnergyHolder _energyHolder;
    public static GameObject energyRecoveryScript;
    private DateTime _timeNew;

    private void Awake()
    {
        _energyMin.gameObject.SetActive(false);
        energyRecoveryScript = GameObject.Find("energyHolderScript");
        _energyHolder = energyRecoveryScript.GetComponent<EnergyHolder>();
    }
    private void OnEnable()
    {
        if (YandexGame.SDKEnabled)
        {
            TimeTextCheck();
        }
    }

    private void TimeTextCheck()
    {
        StartCoroutine(CheckEnergy(new DateTime(YandexGame.savesData.tickStopTimer)));
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
                if (_energyTimeTxt.text == ""||_energyTimeTxt.text == " ")
                {
                    _energyTimeTxt.text = "15";
                }
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
                Debug.Log(_dateTime);

                TimeSpan timeDifference = _dateTime - DateTime.Now;

                if (timeDifference.TotalMinutes < -1)
                {
                    // Если разница отрицательная, значит, прошел новый час
                    // Добавим 60 минут к сохраненному времени
                    _dateTime = _dateTime.AddMinutes(60);
                    timeDifference = _dateTime - DateTime.Now;
                }
                // Используем свойство TotalMinutes для получения разницы в минутах
                int remainingMinutes = (int)timeDifference.TotalMinutes+1;

                _energyTimeTxt.text = $"{remainingMinutes}";
                if (_dateTime<=DateTime.Now)
                {
                    StopCoroutine(CheckEnergy(_dateTime));
                    yield break;
                }
            }
            else
            {
                _energyTimeTxt.gameObject.SetActive(false);
                _energyMin.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
