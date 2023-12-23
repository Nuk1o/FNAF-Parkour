using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LoadingSceneGame : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private GameObject _loadingGO;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private TMP_Text _loadingText;
    private AsyncOperation _async_operation;
    private EnergyHolder _energyHolder;
    public static GameObject energyRecoveryScript;
    
    public void SelectLevelLoading(Button _continue)
    {
        _continue.onClick.AddListener(delegate { ClickLoadScene(); });
    }
    private void ClickLoadScene()
    {
        StartCoroutine("AsyncLoadCOR");
        YandexGame.savesData.isPlay = true;
        energyRecoveryScript = GameObject.Find("energyHolderScript");
        _energyHolder = energyRecoveryScript.GetComponent<EnergyHolder>();
        _energyHolder.MinusEnergy(1);
        Debug.Log("CLICK |||||||||||||||| " + _energyHolder.GetEnergy());
    }
    IEnumerator AsyncLoadCOR()
    {
        _async_operation = SceneManager.LoadSceneAsync(_sceneName);
        _loadingGO.SetActive(true);
        while (!_async_operation.isDone)
        {
            _loadingSlider.value = Mathf.Clamp01(_async_operation.progress / 0.9f);
            _loadingText.text = $"{(_loadingSlider.value * 100).ToString("0")}%";
            yield return true;
        }
    }
}
