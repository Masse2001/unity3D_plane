using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public static class CameraToggle
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera UsingCamera = null;
    public static bool isUsingCamera(CinemachineVirtualCamera camera)
    {
        return camera == UsingCamera;
    }
    public static void SwitchCam(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        UsingCamera = camera;
        foreach (CinemachineVirtualCamera c in cameras)
        {
            if (c != camera && c.Priority!= 0)
            {
                c.Priority = 0;
            }

        }
    }
    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);

    }
    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);

    }
}
