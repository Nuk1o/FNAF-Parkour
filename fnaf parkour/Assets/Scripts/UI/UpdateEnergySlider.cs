using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UpdateEnergySlider : MonoBehaviour
{
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TMP_Text _energySliderText;
    
    private void OnEnable()
    {
        StartCoroutine(CheckEnergy());
    }

    IEnumerator CheckEnergy()
    {
        while (true)
        {
            _energySlider.value = YandexGame.savesData.energy;
            _energySliderText.text = $"{YandexGame.savesData.energy}/5";
            yield return new WaitForSeconds(2);
        }
    }
}
