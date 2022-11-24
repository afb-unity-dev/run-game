using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Afb.RunGame.Presentation.View.Util.InputHelper {
    public static class Pointer {
        public static bool IsPointerOverUIObject(Vector2 position) {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Count > 0;
        }
    }
}
