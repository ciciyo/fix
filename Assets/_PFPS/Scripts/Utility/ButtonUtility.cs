using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HeavenFalls
{
    public class ButtonUtility : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action EventHeld, EventUp;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            EventHeld?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventUp?.Invoke();
        }
    }
}
