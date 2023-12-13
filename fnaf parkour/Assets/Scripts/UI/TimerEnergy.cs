using System;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class TimerEnergy : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyTimeTxt;
    [SerializeField] private TMP_Text _energyMin;

    private DateTime _timeNew;

    private void Awake()
    {
        _energyMin.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(CheckEnergy());
    }

    private void OnDisable()
    {
        StopCoroutine(CheckEnergy());
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

    IEnumerator CheckEnergy()
    {
        while (true)
        {
            if (YandexGame.savesData.energy<5)
            {
                _energyTimeTxt.gameObject.SetActive(true);
                DateTime _minutesTimer = new DateTime(YandexGame.savesData.minutesEnergy);
                Debug.Log(_minutesTimer);
                Debug.Log(_minutesTimer.Minute-DateTime.Now.Minute);
                _energyTimeTxt.text = $"{_minutesTimer.Minute-DateTime.Now.Minute}";
            }
            else
            {
                _energyTimeTxt.gameObject.SetActive(false);
                _energyMin.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(10);
        }
    }
}
