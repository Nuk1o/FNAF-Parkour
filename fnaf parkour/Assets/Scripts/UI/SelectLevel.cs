using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Button _continue;

    public void SelectLvl()
    {
         _continue.onClick.AddListener(OpenNewScene);
    }

    private void OpenNewScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
