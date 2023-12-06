using System;
using TMPro;
using UnityEngine;
using YG;

public class SetLanguage : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _selectLanguage;

    private void Awake()
    {
        YandexGame.savesData.language = "en";
    }
}
