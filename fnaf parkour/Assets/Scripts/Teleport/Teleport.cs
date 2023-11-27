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
    private float _timer;
    private void Awake()
    {
        _closeBtn = _canvas.transform.GetChild(0).transform.gameObject.GetComponent<Button>();
        _canvas.SetActive(false);
    }
    
    void Update()
    {
        _timer += Time.unscaledDeltaTime;
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
        Debug.Log(_timer);
        YandexGame.RequestAuth();
        switch (_sceneName)
        {
            case "Maze1":
                YandexGame.NewLBScoreTimeConvert("TimeToCompleteLevel1", _timer);
                YandexGame.savesData.openLevels[1] = true;
                break;
            case "Maze2":
                YandexGame.NewLBScoreTimeConvert("TimeToCompleteLevel2", _timer);
                YandexGame.savesData.openLevels[2] = true;
                break;
            case "Maze3":
                YandexGame.NewLBScoreTimeConvert("TimeToCompleteLevel3", _timer);
                YandexGame.savesData.openLevels[3] = true;
                break;
            case "Maze4":
                YandexGame.NewLBScoreTimeConvert("TimeToCompleteLevel4", _timer);
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
