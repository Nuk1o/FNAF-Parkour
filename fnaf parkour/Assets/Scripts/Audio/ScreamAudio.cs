using UnityEngine;

public class ScreamAudio : MonoBehaviour
{
    private bool _isScream = false;
    [SerializeField] private AudioSource _audio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _audio != null && !_isScream)
        {
            _isScream = true;
            _audio.Play();
        }
    }
}
