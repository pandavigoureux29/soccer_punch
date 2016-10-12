using UnityEngine;
using System.Collections;
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

    PlayerSpawnManager m_spawnManager;
    Vector3 m_initialPosition;

    public void Awake()
    {
        Empty();
    }
    
    public void OnBeginDrag(PointerEventData _eventData)
    {
        m_initialPosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        this.transform.position = _eventData.position;
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        if( m_spawnManager == null ) 
            m_spawnManager = Component.FindObjectOfType<PlayerSpawnManager>();

        var success = m_spawnManager.SpawnPlayer(pitchPlayerPath);
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
    }

    public void Fill(PlayerDataAsset PlayerData)
    {
        m_fullImage.gameObject.SetActive(true);
        m_emptyImage.gameObject.SetActive(false);
        m_text.gameObject.SetActive(true);
        m_empty = false;
        m_time = 0;
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

    public bool IsEmpty { get { return m_empty; } }
}
