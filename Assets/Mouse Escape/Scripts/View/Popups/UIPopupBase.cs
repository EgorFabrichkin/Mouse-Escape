using UnityEngine;

namespace Mouse_Escape.Scripts.View.Popups
{
    public class UIPopupBase : MonoBehaviour, IPopup
    {
        public void HidePopup()
        {
            AppearDisappearPopup(false);
        }

        public void ShowPopup()
        {
            AppearDisappearPopup(true);
        }
        
        protected void AppearDisappearPopup(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}