using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameGO;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TMP_Text _timerText;
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
                CheckRecord("TimeToCompleteLevel1");
                YandexGame.savesData.openLevels[1] = true;
                break;
            case "Maze2":
                CheckRecord("TimeToCompleteLevel2");
                YandexGame.savesData.openLevels[2] = true;
                break;
            case "Maze3":
                CheckRecord("TimeToCompleteLevel3");
                YandexGame.savesData.openLevels[3] = true;
                break;
            case "Maze4":
                CheckRecord("TimeToCompleteLevel4");
                break;
        }
    }

    private void CheckRecord(string _nameLB)
    {
        string _timeStr = _timer.ToString(@"hh\:mm\:ss\:fff");
        _timerText.text = $"{_timeStr}";
        if (YandexGame.initializedLB)
        {
            YandexGame.GetLeaderboard(_nameLB,20, 3, 3, "Small");
            LBData _lbData = new LBData();
            YandexGame.onGetLeaderboard.Invoke(_lbData);
            // YandexGame.onGetLeaderboard.Invoke(_lbData);
            int uid = Convert.ToInt32(YandexGame.playerId);
            int Userscore = _lbData.players[uid].score;
            if (Userscore<_timer)
            {
                YandexGame.NewLBScoreTimeConvert(_nameLB, _timer);
            }
        }
        else
        {
            YandexGame.NewLBScoreTimeConvert(_nameLB, _timer);
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
