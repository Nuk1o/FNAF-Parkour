using System;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[4];
        public int[] recordsLevels = new int[4];

        // Ваши сохранения

        public int energy = 5;
        public float sensivity = 1;
        public float volumeAudio = 1;
        public long dateTicks = 0;
        public bool isPlay = false;
        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
        
            openLevels[0] = true;
            openLevels[1] = false;
            openLevels[2] = false;
            openLevels[3] = false;

            recordsLevels[0] = 0;
            recordsLevels[1] = 0;
            recordsLevels[2] = 0;
            recordsLevels[3] = 0;
        }
    }
}
