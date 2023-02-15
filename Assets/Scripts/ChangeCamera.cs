using System;
using System.Collections;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Transform cameraWatchPosition;
    private Camera _mainCamera;
    private Vector3 _targetRotation = new Vector3(90.0f, 0.0f, 0.0f);


    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        _mainCamera.transform.SetParent(null);
        MainCamera.UnlockCamera();
        SetCameraToWatch();
    }

    private void SetCameraToWatch()
    {
        //mainCamera.transform.position = cameraWatchPosition.position;
        StartCoroutine(LerpFromTo(transform.position, cameraWatchPosition.position, true, 1.0f));
        StartCoroutine(LerpFromTo(_mainCamera.transform.eulerAngles, _targetRotation, false, 1.0f));
    }

    IEnumerator LerpFromTo(Vector3 currentV3, Vector3 targetV3, bool isPos, float duration) {
        // https://forum.unity.com/threads/moving-the-camera-smoothly.464545/
        for (float t = 0.0f; t < duration; t += Time.deltaTime) {
            if (isPos)
            {
                _mainCamera.transform.position = Vector3.Lerp(currentV3, targetV3, t / duration);
            }
            else
            {
                _mainCamera.transform.eulerAngles = Vector3.Lerp(currentV3, targetV3, t / duration);
            }
            yield return 0;
        }

        if (isPos)
        {
            transform.position = targetV3;
        }
        else
        {
            _mainCamera.transform.eulerAngles = _targetRotation;
        }
    }
}
