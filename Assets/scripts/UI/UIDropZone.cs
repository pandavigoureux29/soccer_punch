using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIDropZone : MonoBehaviour, IDropHandler , IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData _eventData)
    {

    }

    public void OnPointerExit(PointerEventData _eventData)
    {

    }

    public void OnDrop(PointerEventData _eventData)
    {
        Debug.Log(_eventData.position);
        Camera.main.ScreenToWorldPoint(_eventData.position);
    }
}
