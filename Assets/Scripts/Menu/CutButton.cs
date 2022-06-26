
using System;

using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmerSim.Menu
{
    public class CutButton : MonoBehaviour, IGameButton
    {
        public event Action OnPointerDownEvent;
        public event Action OnPointerUpEvent;
        public event Action OnDragEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke();
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