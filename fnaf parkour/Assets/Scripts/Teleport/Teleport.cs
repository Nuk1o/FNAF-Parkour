using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            foreach (var go in _gameGO)
            {
                go.SetActive(false);
            }
            _canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _closeBtn.onClick.AddListener(delegate { ScenLoader("Menu"); });
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
