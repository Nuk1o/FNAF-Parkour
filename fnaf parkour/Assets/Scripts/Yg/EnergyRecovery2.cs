using System;
using System.Threading.Tasks;
using UnityEngine;
using YG;

public class EnergyRecovery2 : MonoBehaviour
{
    private int currentEnergy;    // Текущее количество энергии
    private int minutesUntilRecovery;  // Время до восстановления 1 энергии
    private DateTime lastCheckTime;
    public static EnergyRecovery2 instance;
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
        Debug.Log(YandexGame.savesData.lastLogoutTime);
        currentEnergy = YandexGame.savesData.energy;
        lastCheckTime= LoadLastCheckTime();
        CreateTaskAsync();
    }

    private async Task CheckTimeAsync(string taskName, DateTime startTime, int minutesToWait)
    {
        // Добавляем указанное количество минут к начальному времени
        DateTime endTime = startTime.AddMinutes(minutesToWait);
        YandexGame.savesData.minutesEnergy = endTime.Ticks;
        YandexGame.SaveProgress();
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
                currentEnergy++;
                YandexGame.savesData.energy = currentEnergy;
                Debug.Log(currentEnergy);
                if (currentEnergy<5)
                {
                    lastCheckTime = DateTime.Now;
                    CreateTaskAsync();
                }
            }
            else
            {
                Debug.Log("Максимальная энергия");
            }
        });
    }
    public void UseEnergy()
    {
        currentEnergy--;
        Debug.Log(currentEnergy);
        YandexGame.savesData.energy = currentEnergy;
        lastCheckTime = DateTime.Now;
        CreateTaskAsync();
    }

    private async Task CreateTaskAsync()
    {
        await CheckTimeAsync($"AddEnergy{YandexGame.savesData.energy}", lastCheckTime, 5);
        Debug.Log($"AddEnergy{YandexGame.savesData.energy}");
    }
    
    private DateTime LoadLastCheckTime()
    {
        return new DateTime(YandexGame.savesData.lastLogoutTime);
    }

    private void SaveLastCheckTime()
    {
        YandexGame.savesData.lastLogoutTime = DateTime.Now.Ticks;
        YandexGame.SaveProgress();
        Debug.Log(YandexGame.savesData.lastLogoutTime);
    }

    private void OnApplicationQuit()
    {
        // Сохраняем текущее время при завершении приложения
        SaveLastCheckTime();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        // Сохраняем текущее время при приостановке приложения (например, при сворачивании)
        if (pauseStatus)
        {
            SaveLastCheckTime();
        }
    }
}
