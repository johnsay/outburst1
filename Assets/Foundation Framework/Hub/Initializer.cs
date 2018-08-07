using System.Collections;
using System;
using UnityEngine;

namespace FoundationFramework
{
    public class Initializer : MonoBehaviour
    {
        private enum InitializeEvent
        {
            Awake,
            Start,
            Enable
        }

        [SerializeField] private InitializeEvent _event;
        [Range(0,100),SerializeField] private int _frameBetweenInitializable;
        [SerializeField] private Initializable[] _initializable;
        
        public Action OnInitializationDone;
	
        private void Awake()
        {
            #if !DOTWEEN
              Debug.LogError("Framework not setup, open menu: Tools/Foundation Framework/ToolBar");
              #if UNITY_EDITOR
                UnityEditor. EditorApplication.ExecuteMenuItem("Edit/Play");
              Application.Quit();
              #endif
            #endif
            
            if(_event == InitializeEvent.Awake)
            InitializeAll();
        }
        
        private void Start()
        {
            if(_event == InitializeEvent.Start)
                InitializeAll();
        }
        
        private void OnEnable()
        {
            if(_event == InitializeEvent.Enable)
                InitializeAll();
        }
        

        private void InitializeAll()
        {
            if (_frameBetweenInitializable > 0)
            {
                StartCoroutine(InitializeAllSequence());
            }
            else
            {
                foreach (var component in _initializable)
                {
                    component.Initialize();
                }
                
                FireInitializationDone();
            }

            
        }

        private IEnumerator InitializeAllSequence()
        {
            foreach (var component in _initializable)
            {
                component.Initialize();

                for (int i = 0; i < _frameBetweenInitializable; i++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }

            yield return null;
            FireInitializationDone();
        }

        private void FireInitializationDone()
        {
            if(OnInitializationDone != null)
            OnInitializationDone.Invoke();
        }
    }
}

