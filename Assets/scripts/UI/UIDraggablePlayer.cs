using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDraggablePlayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] string pitchPlayerPath;

    [SerializeField] PlayerDataAsset playerData;

    [SerializeField] string image;

    [SerializeField] GameObject m_fullImage;
    [SerializeField] GameObject m_emptyImage;

    [SerializeField] Text m_text;

    bool m_empty = true;
    float m_time = 0;

    bool m_available = false;

    PlayerSpawnManager m_spawnManager;
    Vector3 m_initialPosition;

    void Awake()
    {
        Empty();
        
    }

    void Update()
    {
        if (!m_empty && !m_available)
        {
            m_time -= Time.deltaTime;
            if( m_time <= 0)
            {
                Activate();
            }
            m_text.text = ""+(int)m_time;
        }
    }
    
    public void OnBeginDrag(PointerEventData _eventData)
    {
        m_initialPosition = transform.localPosition;
        GameObject.Find("AuthorizedDropZone").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        if( m_available )
            this.transform.position = _eventData.position;
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        GameObject.Find("AuthorizedDropZone").GetComponent<SpriteRenderer>().enabled = false;
        if (m_available == false)
            return;

        if( m_spawnManager == null ) 
            m_spawnManager = new List<PlayerSpawnManager>(Component.FindObjectsOfType<PlayerSpawnManager>()) { }.Find(x => x.isLocalPlayer);

        var success = m_spawnManager.SpawnPlayer(playerData);
        if( success)
        {
            Empty();
        }
        transform.localPosition = m_initialPosition;
    }

    public void Empty()
    {
        m_fullImage.gameObject.SetActive(false);
        m_emptyImage.gameObject.SetActive(true);
        m_text.gameObject.SetActive(false);
        m_empty = true;
        m_available = false;
    }

    public void Fill(PlayerDataAsset _playerData)
    {
        m_fullImage.gameObject.SetActive(true);
        m_emptyImage.gameObject.SetActive(false);
        m_text.gameObject.SetActive(true);
        m_empty = false;
        m_time = _playerData.Cooldown;
        playerData = _playerData;
        Deactivate();
    }

    public void Activate()
    {
        m_available = true;
        m_fullImage.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        m_text.gameObject.SetActive(false); 
    }

    public void Deactivate()
    {
        m_fullImage.GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f,0.3f,0.3f);
    }

    public PlayerDataAsset PlayerData
    {
        get
        {
            return playerData;
        }

        set
        {
            playerData = value;
        }
    }

    public bool IsAvailable { get { return m_available; } }

    public bool IsEmpty { get { return m_empty; } }
}
