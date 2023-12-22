using Cinemachine;
using UnityEngine;

public class CamerasInstance : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera thirdPerson;
    [SerializeField] CinemachineVirtualCamera firstPerson;
    public GameObject interior_frame02;
    public GameObject interior_frame03;
    public GameObject windshield01;



    void Update()
    {
        if (Input.GetButtonDown("CameraToggle"))
        {
            if (CameraToggle.isUsingCamera(thirdPerson))
            {
                CameraToggle.SwitchCam(firstPerson);
                Invoke("RemoveWindshield", 1);
            }
            else if (CameraToggle.isUsingCamera(firstPerson))
            {
                CameraToggle.SwitchCam(thirdPerson);
                AddWindshield();
            }
        }
    }
    void RemoveWindshield()
    {
        interior_frame03.SetActive(false);
        interior_frame02.SetActive(false);
        windshield01.SetActive(false);
    }

    void AddWindshield()
    {
        interior_frame03.SetActive(true);
        interior_frame02.SetActive(true);
        windshield01.SetActive(true);
    }
    private void OnEnable()
    {
        CameraToggle.Register(thirdPerson);
        CameraToggle.Register(firstPerson);
        CameraToggle.SwitchCam(thirdPerson);
    }

    private void OnDisable()
    {
        CameraToggle.Unregister(thirdPerson);
        CameraToggle.Unregister(firstPerson);
    }
}