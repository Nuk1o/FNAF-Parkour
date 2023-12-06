using System;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class TimerEnergy : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyTimeTxt;

    private DateTime _timeNew;
    private void OnEnable()
    {
        StartCoroutine(CheckEnergy());
    }

    private void OnDisable()
    {
        StopCoroutine(CheckEnergy());
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
                    _energyTimeTxt.text = $"{_newDateTime.Minute} мин.";
                }
                else
                {
                    _energyTimeTxt.gameObject.SetActive(false);
                }
            }
            yield return new WaitForSeconds(10);
        }
    }
}
