using UnityEngine;
using System.Collections; 

public class CameraSwitcher : MonoBehaviour {

    [SerializeField] Camera cameraMain;
    [SerializeField] Camera cameraSecond;

    public void SwitchCameras(bool main)
    {
        cameraMain.gameObject.SetActive(main);
        cameraSecond.gameObject.SetActive(!main);

        //if (!main)
        //{
        //    var landscapeGO = GameObject.Find("landscape");
        //    for (int i = 0; i < landscapeGO.transform.GetChildCount(); i++)
        //    {
        //        var child = landscapeGO.transform.GetChild(i);
        //        child.transform.Rotate(new Vector3(0, 0, 180));
        //    }
        //}
    }
}
