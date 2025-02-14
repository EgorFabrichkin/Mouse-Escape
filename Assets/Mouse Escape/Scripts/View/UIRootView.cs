using UnityEngine;

namespace Mouse_Escape.Scripts.View
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private Transform uiSceneContainer;
        [SerializeField] private GameObject loadingScreen;
        
        //[SerializeField] private AudioSource mainTheme;
        
        private void Awake()
        {
            HideLoadingScreen();
        }

        public void PlayMusic()
        {
            //mainTheme.Play();
        }
        
        public void ShowLoadingScreen() => loadingScreen.SetActive(true);

        public void HideLoadingScreen() => loadingScreen.SetActive(false);

        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearContainer();
            
            sceneUI.transform.SetParent(uiSceneContainer, false);
        }

        private void ClearContainer()
        {
            var childCount = uiSceneContainer.childCount;
            for (var i = 0; i < childCount; i++)
            {
                Destroy(uiSceneContainer.GetChild(i).gameObject);   
            }
        }
    }
}