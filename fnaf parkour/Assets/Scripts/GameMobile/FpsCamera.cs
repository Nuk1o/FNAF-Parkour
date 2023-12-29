using UnityEngine;
using Cinemachine;

public class FpsCamera : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _camera;
    public TouchArea fixedtouch; 
    [Range(0f, 5f)]
    public float Sensitivity;
    //private Rigidbody rbPlayer;
    private float lookVer, lookHor; 

    void Start()
    {
        
        //rbPlayer = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked; 

    }

    void Update()
    {
        if(gameObject.tag == "MainCamera")
        {
            LookUp();
        }else if(gameObject.tag == "Player")
        {
            lookHorF(); 
        }
    }

    void LookUp()
    {
        lookVer = fixedtouch.touchDist.y * Sensitivity; 
        //transform.localEulerAngles -= new Vector3(lookVer, 0, 0); 
        float lookVer2 = Mathf.Clamp(lookVer,-90,90);
        transform.localEulerAngles -= new Vector3(lookVer, 0, 0);
    }

    void lookHorF()
    {
        lookHor += fixedtouch.touchDist.x * Sensitivity;

        transform.localEulerAngles = new Vector3(0,lookHor, 0);
    }
}
