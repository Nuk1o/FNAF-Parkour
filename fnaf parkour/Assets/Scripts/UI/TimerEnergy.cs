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
                
                _timeNew = new DateTime(YandexGame.savesData.dateTicks);
                Debug.Log("Время: "+ new DateTime(YandexGame.savesData.dateTicks));
                Debug.Log("Время сейчас: "+new DateTime(DateTime.Now.Ticks));
                if (new DateTime(DateTime.Now.Ticks)<_timeNew)
                {
                    _energyTimeTxt.gameObject.SetActive(true);
                    DateTime _nowDateTime = new DateTime(DateTime.Now.Ticks);
                    DateTime _newDateTime = new DateTime(_timeNew.Ticks - _nowDateTime.Ticks);
                    _energyTimeTxt.text = $"{_newDateTime.Minute}";
                }
                else
                {
                    _energyTimeTxt.gameObject.SetActive(false);
                    _energyMin.gameObject.SetActive(false);
                }
            }
            yield return new WaitForSeconds(10);
        }
    }
}
