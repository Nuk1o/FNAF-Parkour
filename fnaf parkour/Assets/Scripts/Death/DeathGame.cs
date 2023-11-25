using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class DeathGame : MonoBehaviour
{
    [SerializeField] private GameObject _freddy;
    [SerializeField] private GameObject _gameOverCanvas;
    private Button _buttonClose;
    public bool _isRunScript = false;

    private void Start()
    {
        _buttonClose = _gameOverCanvas.transform.GetChild(0).GetComponent<Button>();
        _buttonClose.onClick.AddListener(delegate { ClickBtn(); });
        YandexGame.savesData.energy--;
        YandexGame.SaveProgress();
    }

    private void Update()
    {
        if (_isRunScript)
        {
            Death();
        }
    }

    public void Death()
    {
        _isRunScript = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _freddy.gameObject.SetActive(false);
        _gameOverCanvas.SetActive(true);
    }

    private void ClickBtn()
    {
        Debug.Log("Нажал на кнопку. Рекламная пауза");
        YandexGame.FullscreenShow();
        _buttonClose.enabled = false;
        SceneManager.LoadScene("Menu");
    }
}
