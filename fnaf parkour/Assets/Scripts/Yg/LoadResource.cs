using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LoadResource : MonoBehaviour
{
    [Header("Level Set Menu")]
    private int _energyInt;
    private bool[] _levelsActive;
    private bool _isActive = true;
    [SerializeField] private Button[] _levels;
    [SerializeField] private Button _btnAD;
    [SerializeField] private Slider _energy;
    [SerializeField] private TMP_Text _energyTXT;
    [SerializeField] private GameObject _levelSet;
    [SerializeField] private Sprite _levelActiveSprite;

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
        }
    }

    public void GetLoad()
    {
        _energyInt = YandexGame.savesData.energy;
        _levelsActive = YandexGame.savesData.openLevels;
    }

    private void LevelSetActive()
    {
        if (_levelSet.activeInHierarchy)
        {
            OpenLevel();
            //EnergySet();
            Debug.Log($"Энергия: {_energyInt}");
        }
    }
    
    private void EnergySet()
    {
        _energy.value = _energyInt;
        _energyTXT.text = $"{_energyInt}/5";
        if (_energy.value == 0)
        {
            _btnAD.gameObject.SetActive(true);
            for (int i = 0; i < _levelsActive.Length; i++)
            {
                if (_levelsActive[i])
                {
                    _levels[i].interactable = false;
                }
            }
            Debug.Log("Нет энергии");
        }
        else
        {
            _btnAD.gameObject.SetActive(false);
        }
    }

    private void OpenLevel()
    {
        try
        {
            for (int i = 0; i < _levelsActive.Length; i++)
            {
                if (_levelsActive[i])
                {
                    _levels[i].interactable = true;
                    _levels[i].transform.gameObject.GetComponent<Image>().sprite = _levelActiveSprite;
                }
            }
        }
        catch
        {
            Debug.Log("Ошибка | OpenLevelButton");
        }
        
    }
}
