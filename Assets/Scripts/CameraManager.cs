using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : UnitySingleton<CameraManager>
{
    public CinemachineVirtualCamera currentCamera;
    public void StartShake(float str=1f, float dur=1f, float freq=1f, bool perma=false) {
        CameraShake shake = currentCamera.GetComponent<CameraShake>();
        if (shake) {
            shake.StartShake(str, dur, freq, perma);
        }
    }
}
