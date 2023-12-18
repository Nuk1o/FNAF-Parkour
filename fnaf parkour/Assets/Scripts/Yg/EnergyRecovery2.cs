using System;
using System.Threading.Tasks;
using UnityEngine;
using YG;

public class EnergyRecovery2 : MonoBehaviour
{
    private int currentEnergy;    // Текущее количество энергии
    private int minutesUntilRecovery;  // Время до восстановления 1 энергии
    private DateTime lastCheckTime;
    private DateTime[] lastCheckTimeArr = new DateTime[5];
    private int _timeToEnergy = 15;//Сколько нужно ждать в минутах
    public static EnergyRecovery2 instance;
    public static GameObject energyRecoveryScript;

    private int energySum = 0;

    private bool _isActiveEnergyRecovery = false;
    
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
        CheckTime();
    }

    public void CheckTime()
    {
        currentEnergy = YandexGame.savesData.energy;
        for (int i = 0; i < 5; i++)
        {
            if (i!=4 && YandexGame.savesData.lastLogOutTime[i+1] != 0)
            {
                YandexGame.savesData.lastLogOutTime[i] = 0;
                YandexGame.SaveProgress();
                Debug.Log("Обнулил");
            }
            Debug.Log(YandexGame.savesData.lastLogOutTime[i]);
            energySum += (int)YandexGame.savesData.lastLogOutTime[i];
            lastCheckTimeArr[i]= LoadLastCheckTime($"AddEnergy{i}");
            Debug.Log("ВРЕМЯ | "+lastCheckTimeArr[i].AddMinutes(_timeToEnergy));
            Debug.Log($"Index {i}");
        }

        if (energySum ==0)
        {
            YandexGame.savesData.energy = 5;
            YandexGame.SaveProgress();
        }

        for (int j = 0; j < 5; j++)
        {
            if (YandexGame.savesData.lastLogOutTime[j] != 0)
            {
                CreateTaskAsync($"AddEnergy{j}");
                Debug.Log($"Start | AddEnergy{j}");
            }
        }
    }

    private async Task CheckTimeAsync(int index,string taskName, DateTime startTime,DateTime endTimes, int minutesToWait)
    {
        DateTime endTime = new DateTime();
        if (endTimes != new DateTime())
        {
            endTime = endTimes;
        }
        else
        {
            endTime = startTime.AddMinutes(minutesToWait);
        }
        YandexGame.savesData.endOutTime[index] = endTime.Ticks;
        YandexGame.SaveProgress();
        Debug.Log($"SAVE |||||||| {index}  = {new DateTime(YandexGame.savesData.endOutTime[index]) }");
        Debug.Log(endTime);
        Debug.Log(taskName);
        Debug.Log(minutesToWait);
        // Ожидаем асинхронно, пока не пройдет нужное время
        await Task.Run(async () =>
        {
            while (DateTime.Now < endTime)
            {
                await Task.Delay(1000); // Пауза в 1 секунду (1000 миллисекунд)
            }
            Debug.Log($"{taskName}: Прошло {minutesToWait} минут!");
            if (currentEnergy < 5)
            {
                ClearEnergySave($"AddEnergy{YandexGame.savesData.energy}");
                currentEnergy++;
                YandexGame.savesData.energy = currentEnergy;
                Debug.Log(currentEnergy);
                if (currentEnergy<5)
                {
                    lastCheckTime = DateTime.Now;
                    //CreateTaskAsync($"AddEnergy{YandexGame.savesData.energy}");
                    Debug.Log($"CheckTimeAsync | AddEnergy{YandexGame.savesData.energy}");
                }
                
            }
            else
            {
                Debug.Log("Максимальная энергия");
            }
        });
    }

    private void ClearEnergySave(string nameEnergy)
    {
        switch (nameEnergy)
        {
            case "AddEnergy0":
                YandexGame.savesData.lastLogOutTime[0] = 0;
                YandexGame.savesData.endOutTime[0] = 0;
                break;
            case "AddEnergy1":
                YandexGame.savesData.lastLogOutTime[1] = 0;
                YandexGame.savesData.endOutTime[1] = 0;
                break;
            case "AddEnergy2":
                YandexGame.savesData.lastLogOutTime[2] = 0;
                YandexGame.savesData.endOutTime[2] = 0;
                break;
            case "AddEnergy3":
                YandexGame.savesData.lastLogOutTime[3] = 0;
                YandexGame.savesData.endOutTime[3] = 0;
                break;
            case "AddEnergy4":
                YandexGame.savesData.lastLogOutTime[4] = 0;
                YandexGame.savesData.endOutTime[4] = 0;
                break;
        }
        YandexGame.SaveProgress();
    }
    
    public void UseEnergy()
    {
        currentEnergy--;
        Debug.Log(currentEnergy);
        YandexGame.savesData.energy = currentEnergy;
        lastCheckTime = DateTime.Now;
        CreateTaskAsync($"AddEnergy{YandexGame.savesData.energy}");
        Debug.Log($"UseEnergy | AddEnergy{YandexGame.savesData.energy}");
        YandexGame.SaveProgress();
    }

    private async Task CreateTaskAsync(string nameAsync)
    {
        switch (nameAsync)
        {
            case "AddEnergy0":
                if (lastCheckTimeArr[0] != new DateTime())
                {
                    await CheckTimeAsync(0,$"AddEnergy0", new DateTime(YandexGame.savesData.lastLogOutTime[0]), 
                        new DateTime(YandexGame.savesData.endOutTime[0]),_timeToEnergy);
                }
                break;
            case "AddEnergy1":
                if (lastCheckTimeArr[1] != new DateTime())
                {
                    await CheckTimeAsync(1,$"AddEnergy1", new DateTime(YandexGame.savesData.lastLogOutTime[1]), 
                        new DateTime(YandexGame.savesData.endOutTime[1]),_timeToEnergy);
                }
                break;
            case "AddEnergy2":
                if (lastCheckTimeArr[2] != new DateTime())
                {
                    await CheckTimeAsync(2,$"AddEnergy2", new DateTime(YandexGame.savesData.lastLogOutTime[2]),
                        new DateTime(YandexGame.savesData.endOutTime[2]), _timeToEnergy);
                }
                break;
            case "AddEnergy3":
                if (lastCheckTimeArr[3] != new DateTime())
                {
                    await CheckTimeAsync(3,$"AddEnergy3", new DateTime(YandexGame.savesData.lastLogOutTime[3]),
                        new DateTime(YandexGame.savesData.endOutTime[3]),_timeToEnergy);
                }
                break;
            case "AddEnergy4":
                if (lastCheckTimeArr[4] != new DateTime())
                {
                    await CheckTimeAsync(4,$"AddEnergy4", new DateTime(YandexGame.savesData.lastLogOutTime[4]),
                        new DateTime(YandexGame.savesData.endOutTime[4]),_timeToEnergy);
                }
                break;
        }
    }
    
    private DateTime LoadLastCheckTime(string name)
    {
        switch (name)
        {
            case "AddEnergy0":
                return new DateTime(YandexGame.savesData.lastLogOutTime[0]);
            case "AddEnergy1":
                return new DateTime(YandexGame.savesData.lastLogOutTime[1]);
            case "AddEnergy2":
                return new DateTime(YandexGame.savesData.lastLogOutTime[2]);
            case "AddEnergy3":
                return new DateTime(YandexGame.savesData.lastLogOutTime[3]);
            case "AddEnergy4":
                return new DateTime(YandexGame.savesData.lastLogOutTime[4]);
        }
        return DateTime.Now;
    }

    private void SaveLastCheckTime(string name)
    {
        switch (name)
        {
            case "AddEnergy0":
                YandexGame.savesData.lastLogOutTime[0] = DateTime.Now.Ticks;
                Debug.Log(YandexGame.savesData.lastLogOutTime[4]);
                break;
            case "AddEnergy1":
                YandexGame.savesData.lastLogOutTime[1] = DateTime.Now.Ticks;
                Debug.Log(YandexGame.savesData.lastLogOutTime[4]);
                break;
            case "AddEnergy2":
                YandexGame.savesData.lastLogOutTime[2] = DateTime.Now.Ticks;
                Debug.Log(YandexGame.savesData.lastLogOutTime[4]);
                break;
            case "AddEnergy3":
                YandexGame.savesData.lastLogOutTime[3] = DateTime.Now.Ticks;
                Debug.Log(YandexGame.savesData.lastLogOutTime[4]);
                break;
            case "AddEnergy4":
                YandexGame.savesData.lastLogOutTime[4] = DateTime.Now.Ticks;
                Debug.Log(YandexGame.savesData.lastLogOutTime[4]);
                break;
        }
        YandexGame.SaveProgress();
    }

    private void OnApplicationQuit()
    {
        // Сохраняем текущее время при завершении приложения
        SaveLastCheckTime($"AddEnergy{YandexGame.savesData.energy}");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        // Сохраняем текущее время при приостановке приложения (например, при сворачивании)
        if (pauseStatus)
        {
            SaveLastCheckTime($"AddEnergy{YandexGame.savesData.energy}");
        }
    }
}
