using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    
    public void SceneLoading()
    {
        SceneManager.LoadScene(_sceneName);
    }
    
    public void SelectLevelLoading(Button _continue)
    {
        _continue.onClick.AddListener(delegate { SceneLoading(); });
        YandexGame.savesData.energy--;
        YandexGame.SaveProgress();
    }
}
