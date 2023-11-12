using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void SceneLoading()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
