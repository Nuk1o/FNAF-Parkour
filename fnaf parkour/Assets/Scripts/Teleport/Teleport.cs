using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string _nameScene;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ВЫ ВЫГРАЛ");
        if (other.tag == "Player" && _nameScene != null)
        {
            SceneManager.LoadScene(_nameScene);
        }
    }
}
