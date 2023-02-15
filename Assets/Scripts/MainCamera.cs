using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    public static bool cameraShouldBeLocked;
    private Vector3 _offset;
    private void Start()
    {
        _offset = transform.position - player.transform.position;
        cameraShouldBeLocked = true;
    }
    private void LateUpdate()
    {
        if (cameraShouldBeLocked)
        {
            transform.position = player.transform.position + _offset;
        }
    }

    public static void UnlockCamera()
    {
        cameraShouldBeLocked = false;
    }
}