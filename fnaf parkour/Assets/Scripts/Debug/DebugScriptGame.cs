using YG;
using UnityEngine;

public class DebugScriptGame : MonoBehaviour
{
    private void Start()
    {
        YandexGame.savesData.energy = 5;
        YandexGame.savesData.openLevels[0] = true;
        YandexGame.savesData.openLevels[1] = true;
        YandexGame.savesData.openLevels[2] = true;
        YandexGame.savesData.openLevels[3] = true;
        YandexGame.SaveProgress();
    }
}
