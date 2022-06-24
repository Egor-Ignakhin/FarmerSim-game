
using System;

using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmerSim.Menu
{
    public class CutWheatButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        internal event Action OnPointerDownEvent;
        internal event Action OnPointerUpEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent?.Invoke();
        }

        private void OnDestroy()
        {
            OnPointerDownEvent = null;
            OnPointerUpEvent = null;
        }
    }
}