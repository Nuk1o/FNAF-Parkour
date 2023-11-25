using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LoadResource : MonoBehaviour
{
    [Header("Level Set Menu")]
    private int _energyInt, _lvlInt;
    private bool _isActive = true;
    [SerializeField] private Button[] _levels;
    [SerializeField] private Button _btnAD;
    [SerializeField] private Slider _energy;
    [SerializeField] private TMP_Text _energyTXT;
    [SerializeField] private GameObject _levelSet;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    private void Update()
    {
        if (_levelSet.activeSelf && _isActive)
        {
            _isActive = false;
            LevelSetActive();
            Debug.Log("TESTTTTTTT");
        }
    }

    public void GetLoad()
    {
        _energyInt = YandexGame.savesData.energy;
        _lvlInt = YandexGame.savesData.lvl;
    }

    private void LevelSetActive()
    {
        if (_levelSet.activeInHierarchy)
        {
            EnergySet();
            OpenLevel();
            Debug.Log($"Энергия: {_energyInt}");
            Debug.Log($"Уровень: {_lvlInt}");
        }
    }
    
    private void EnergySet()
    {
        _energy.value = _energyInt;
        _energyTXT.text = $"{_energyInt}/5";
        if (_energy.value == 0)
        {
            _btnAD.gameObject.SetActive(true);
        }
        else
        {
            _btnAD.gameObject.SetActive(false);
        }
    }

    private void OpenLevel()
    {
        for (int i = 1; i < _levels.Length; i++)
        {
            if (i <= _lvlInt)
            {
                _levels[i--].enabled = true;
            }
        }
    }
    
    
}
