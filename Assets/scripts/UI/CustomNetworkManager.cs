using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CustomNetworkManager : MonoBehaviour {

    [SerializeField] InputField m_inputfield;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnStartHostButtonClicked()
    {
        NetworkManager.singleton.StartHost();
        this.gameObject.SetActive(false);
    }

    public void OnStartClientButtonClicked()
    {
        NetworkManager.singleton.networkAddress = m_inputfield.text ;
        NetworkManager.singleton.StartClient();
        this.gameObject.SetActive(false);
    }
    
}
