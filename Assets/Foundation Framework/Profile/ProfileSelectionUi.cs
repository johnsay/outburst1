
using System;
using System.Collections.Generic;
#if DOTWEEN
using DG.Tweening;
    #endif
using UnityEngine;
using UnityEngine.UI;
using Utils;
using FoundationFramework.UI;

namespace FoundationFramework
{
    public class ProfileSelectionUi : MonoBehaviour
    {
        [SerializeField] private UiPanelBase _createProfilePanel;
        [SerializeField] private UiPanelBase _profileListPanel;
        [SerializeField] private LayoutGroup _layoutGroup;
        [SerializeField] private ProfileEntryUi _profilePrefab;
        [SerializeField] private ProfileSelection _profileSelection;

        [Header("Selected profile: ")] 
        [SerializeField] private RectTransform _selectedRect;
        [SerializeField] private Text _selectedName;
#if DOTWEEN
        private Sequence _selectedSequence;
        #endif
        [SerializeField] private InputField _profileInput;
        private GenericUiList<string> _profiles;

        private void Awake()
        {
            _profiles = new GenericUiList<string>(_profilePrefab.gameObject,_layoutGroup);
        }
        
        public void SetProfileList(IEnumerable<string> data)
        {
           _profiles.Generate<ProfileEntryUi>(data, (entry, item) => { item.Setup(entry); });
        }

        public void InitialOpenProfileCreation()
        {
            _profileInput.text = string.Empty;
            _profileListPanel.Hide(false);
            _createProfilePanel.Show(false);
        }

        public void OpenProfileCreation()
        {
            _profileInput.text = string.Empty;
            _profileListPanel.Hide();
            _createProfilePanel.Show();
        }

        public void InitialOpenProfileList()
        {
            _profileListPanel.Show(false);
            _createProfilePanel.Hide(false); 
        }

        public void OpenProfileList()
        {
            _profileListPanel.Show();
            _createProfilePanel.Hide();
        }

        #region Ui Input
        public void ClickCreateProfileName()
        {
            if (string.IsNullOrEmpty(_profileInput.text))
            {
                var msg = DialogBoxData.CreateInfo("Profile name cant be empty");
                UiDialogBox.Instance.ShowDialog(msg);
                return;
            }
            
            _profileSelection.TryCreateProfile(_profileInput.text);
        }

        public void ClickDeleteSelectedProfile()
        {
            _profileSelection.DeleteProfile();
            HideSelected();

        }

        #region Selected

        private void TryKillSelectedTween()
        {
#if DOTWEEN
            if(_selectedSequence != null && _selectedSequence.IsPlaying())
                _selectedSequence.Kill();
    #endif
        }

        public void StartSelectedSequence(string selected)
        {
#if DOTWEEN
            TryKillSelectedTween();
            _selectedSequence = DOTween.Sequence();
            if (_selectedRect.localScale.x > 0)
            {
                Tweener tweenClose = _selectedRect.DOScaleX(0, 0.15f);
                tweenClose.OnComplete(() => _selectedName.text = selected);
                _selectedSequence.Append(tweenClose);
            }
            else
            {
                _selectedName.text = selected;
            }


            Tweener tweenOpen = _selectedRect.DOScaleX(1, 0.15f);
            _selectedSequence.Append(tweenOpen);
            _selectedSequence.Play();
    #endif
        }

        private void HideSelected()
        {
#if DOTWEEN
            TryKillSelectedTween();
            _selectedSequence = DOTween.Sequence();
             Tweener tweenClose = _selectedRect.DOScaleX(0, 0.15f);
             tweenClose.OnComplete(() => _selectedName.text = String.Empty);
             _selectedSequence.Append(tweenClose);
            _selectedSequence.Play();
    #endif
        }

        #endregion
        #endregion
        
    }
}
