
using System;

using UnityEngine.EventSystems;

namespace FarmerSim.Menu
{
    public interface IGameButton : IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        event Action OnPointerDownEvent;
        event Action OnPointerUpEvent;
        event Action OnDragEvent;
    }
}