using System;
using YG;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnergyHolder : MonoBehaviour
{
    public int _energy { get; private set; }
    public static EnergyHolder instance;
    public static GameObject energyRecoveryScript;
    private DateTime _startTimer,_endTimer;
    
    void Awake()
    {
        energyRecoveryScript = GameObject.Find("energyHolderScript");
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
        if (YandexGame.SDKEnabled)
        {
            _startTimer = new DateTime();
            _endTimer = new DateTime();
            _energy = YandexGame.savesData.energy;
            RecoveryEnergy();
        }
    }

    public int GetEnergy()
    {
        return _energy;
    }

    public void MinusEnergy(int energy)
    {
        if (_energy>0)
        {
            _energy = _energy - energy;
        }
        SaveEnergy();
        RecoveryEnergy();
    }
    
    public void PlusEnergy(int energy)
    {
        if (_energy>0)
        {
            _energy = _energy + energy;
        }
        SaveEnergy();
    }

    public void SaveEnergy()
    {
        YandexGame.savesData.energy = _energy;
        YandexGame.SaveProgress();
    }

    public void ResetEnd()
    {
        YandexGame.savesData.tickStopTimer = 0;
        YandexGame.savesData.tickStartTimer = 0;
        if (_energy<5)
        {
            RecoveryEnergy();
        }
    }

    private async void RecoveryEnergy()
    {
        if (YandexGame.savesData.tickStartTimer == 0)
        {
            _startTimer = DateTime.Now;
            YandexGame.savesData.tickStartTimer = _startTimer.Ticks;
        }
        else
        {
            _startTimer = new DateTime(YandexGame.savesData.tickStartTimer);
        }
        
        //Если у нас энергия меньше чем 5
        if (_energy < 5)
        {
            _endTimer = _startTimer.AddMinutes(15);
            Debug.Log("ВРЕМЯ ОКОНЧАНИЯ ||||||||||||||| " + _endTimer);
            await TimerRecovery(_endTimer);
        }
        else
        {
            Debug.Log("Всё хорошо энергия полная");
        }
    }
    
    private async UniTask TimerRecovery(DateTime endTime)
    {
        if (YandexGame.SDKEnabled)
        {
            Debug.Log("Начал async task");
            Debug.Log(endTime);
            Debug.Log(YandexGame.savesData.tickStopTimer);
            Debug.Log(DateTime.Now);
            if (YandexGame.savesData.tickStopTimer == 0)
            {
                YandexGame.savesData.tickStopTimer = endTime.Ticks;
                YandexGame.SaveProgress();
                Debug.Log("Save tickTimer");
            }
        
            while (true)
            {
                if (DateTime.Now < endTime)
                {
                    await UniTask.Delay(5000);
                    Debug.Log("Подождал ++++++++++++++++++++++++++");
                }
                else
                {
                    PlusEnergy(1);
                    ResetEnd();
                    Debug.Log("Закончил");
                    if (GetEnergy() < 5)
                    {
                        RecoveryEnergy();
                        Debug.Log("Закончил async task");
                    }
                    break;
                }
            }
        }
        else
        {
            Debug.Log("SDK не включен");
        }
    }
    
    // async Task TimerRecovery1(DateTime endTime)
    // {
    //     if (YandexGame.SDKEnabled)
    //     {
    //         Debug.Log("Начал async task");
    //         Debug.Log(endTime);
    //         Debug.Log(YandexGame.savesData.tickStopTimer);
    //         Debug.Log(DateTime.Now);
    //         if (YandexGame.savesData.tickStopTimer == 0)
    //         {
    //             YandexGame.savesData.tickStopTimer = endTime.Ticks;
    //             YandexGame.SaveProgress();
    //         }
    //
    //         if (DateTime.Now < endTime)
    //         {
    //             await Task.Delay(5000);
    //             Debug.Log("Подождал ++++++++++++++++++++++++++");
    //         }
    //         else
    //         {
    //             PlusEnergy(1);
    //             ResetEnd();
    //             Debug.Log("Закончил");
    //             if (GetEnergy() < 5)
    //             {
    //                 RecoveryEnergy();
    //             }
    //         }
    //         Debug.Log("Закончил async task");
    //     }
    //     else
    //     {
    //         Debug.Log("SDK не включен");
    //     }
    // }
    
    
}
