using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIDraggablePlayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] string pitchPlayerPath;

    PlayerSpawnManager m_spawnManager;
    Vector3 m_initialPosition;
        
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
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = m_initialPosition;
        }
    }
}
