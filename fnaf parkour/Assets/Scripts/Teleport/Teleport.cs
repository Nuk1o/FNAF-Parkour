using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string _nameScene;
    [SerializeField] private GameObject[] _gameGO;
    [SerializeField] private GameObject _canvas;
    private Button _closeBtn;
    private void Awake()
    {
        _closeBtn = _canvas.transform.GetChild(0).transform.gameObject.GetComponent<Button>();
        _canvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SaveData();
            foreach (var go in _gameGO)
            {
                go.SetActive(false);
            }
            _canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _closeBtn.onClick.AddListener(delegate { ScenLoader("Menu"); });
            YandexGame.savesData.isPlay = false;
            YandexGame.SaveProgress();
        }
    }

    private void SaveData()
    {
        Scene scene = SceneManager.GetActiveScene();
        string _sceneName = scene.name;
        switch (_sceneName)
        {
            case "Maze1":
                YandexGame.savesData.openLevels[1] = true;
                break;
            case "Maze2":
                YandexGame.savesData.openLevels[2] = true;
                break;
            case "Maze3":
                YandexGame.savesData.openLevels[3] = true;
                break;
        }
    }

    private void ScenLoader(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Пустое значение sceneName");
        }
    }
}
