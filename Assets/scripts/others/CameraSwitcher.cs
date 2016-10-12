using UnityEngine;
using System.Collections; 

public class CameraSwitcher : MonoBehaviour {

    [SerializeField] Camera cameraMain;
    [SerializeField] Camera cameraSecond;

    public void SwitchCameras(bool main)
    {
        cameraMain.gameObject.SetActive(main);
        cameraSecond.gameObject.SetActive(!main);
    }
}
