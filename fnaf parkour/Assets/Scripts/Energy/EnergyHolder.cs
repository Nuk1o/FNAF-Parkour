using System;
using System.Threading.Tasks;
using YG;
using UnityEngine;

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

    private void RecoveryEnergy()
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
            TimerRecovery(_endTimer);
        }
        else
        {
            Debug.Log("Всё хорошо энергия полная");
        }
    }


    async Task TimerRecovery(DateTime endTime)
    {
        if (YandexGame.savesData.tickStopTimer == 0)
        {
            YandexGame.savesData.tickStopTimer = endTime.Ticks;
            YandexGame.SaveProgress();
        }
        while (true)
        {
            if (DateTime.Now != new DateTime(YandexGame.savesData.tickStopTimer))
            {
                await Task.Delay(5000);
                Debug.Log("SaveProgress");
            }
            else
            {
                PlusEnergy(1);
                Debug.Log("Закончил");
                if (GetEnergy() < 5)
                {
                    RecoveryEnergy();
                }
                return;
            }
        }
        
    }
}
