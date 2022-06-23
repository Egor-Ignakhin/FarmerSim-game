
using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FarmerSim.Player
{
    public class JoystickManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image imgJoystickBG;
        [SerializeField] private Image imgJoystick;
        private Vector2 posInput;

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                imgJoystickBG.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out posInput))
            {
                posInput /= imgJoystickBG.rectTransform.sizeDelta;

                if (posInput.magnitude > 1)
                {
                    posInput = posInput.normalized;
                }

                imgJoystick.rectTransform.anchoredPosition = (posInput * imgJoystickBG.rectTransform.sizeDelta.x / 2);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            posInput = Vector2.zero;
            imgJoystick.rectTransform.anchoredPosition = posInput;
        }

        internal float GetHorizontal()
        {
            if (posInput.x != 0)
            {
                return posInput.x;
            }
            else
            {
                return Input.GetAxis("Horizontal");
            }
        }

        internal float GetVertical()
        {
            if (posInput.y != 0)
            {
                return posInput.y;
            }
            else
            {
                return Input.GetAxis("Vertical");
            }
        }
    }
}