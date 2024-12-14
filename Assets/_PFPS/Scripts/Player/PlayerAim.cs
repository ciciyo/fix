using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CinemachineFreeLook camFreeLook;
    public float sensitivity = 0.25f;
    
    private Image _aimControl;
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private void Start()
    {
        _aimControl = GetComponent<Image>();
        camFreeLook.m_YAxis.m_MaxSpeed *= sensitivity;
        camFreeLook.m_XAxis.m_MaxSpeed *= sensitivity;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _aimControl.rectTransform,
                eventData.position,
                eventData.enterEventCamera,
                out var pos))
        {
            camFreeLook.m_XAxis.m_InputAxisName = MouseX;
            camFreeLook.m_YAxis.m_InputAxisName = MouseY;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        camFreeLook.m_XAxis.m_InputAxisName = null;
        camFreeLook.m_YAxis.m_InputAxisName = null;
        camFreeLook.m_XAxis.m_InputAxisValue = 0;
        camFreeLook.m_YAxis.m_InputAxisValue = 0;
    }
}
