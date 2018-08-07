
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework
{
    public sealed class SceneLoadingAnimation : MonoBehaviour 
    {
        #region [[ FIELDS ]]

        [SerializeField] private SceneLoader _sceneLoader;

        [SerializeField] private Slider _loadingBar;

        #endregion

        private void Awake()
        {
            _sceneLoader.OnStartLoadScene += OnStartLoadScene;
            _sceneLoader.OnLoadingProgress += OnLoaderProgress;
            _sceneLoader.OnFinishLoadScene += OnFinishLoadScene;
            SetSliderValue(0);
        }

        private void OnDestroy()
        {
            _sceneLoader.OnStartLoadScene -= OnStartLoadScene;
            _sceneLoader.OnLoadingProgress -= OnLoaderProgress;
            _sceneLoader.OnFinishLoadScene -= OnFinishLoadScene;
        }

        private void OnStartLoadScene()
        {
            SetSliderValue(0);
        }

        private void OnLoaderProgress(float progress)
        {
            SetSliderValue(progress);
        }

        private void OnFinishLoadScene()
        {
            SetSliderValue(1);
        }


        private void SetSliderValue(float progress)
        {
            _loadingBar.value = progress;
        }

    }
}
