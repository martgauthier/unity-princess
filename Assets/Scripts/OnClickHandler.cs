using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClickHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onClickCallback;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        onClickCallback.Invoke();
    }
}
