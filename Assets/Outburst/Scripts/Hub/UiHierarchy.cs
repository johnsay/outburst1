using System.Collections.Generic;
using TreeView;
using UnityEngine;
using UnityEngine.UI;

public class UiHierarchy : MonoBehaviour
{
	#region FIELDS
	public static UiHierarchy Instance;
	[SerializeField] private LayoutGroup _layout;
	[SerializeField] private LayoutGroup _layoutGridPrefab;
	
	public ContentTree Content;
	public UiSection SectionPrefab;
	public UiNext NextPrefab;
	public UiQuestionPack QuestionPackPrefab;

	private List<UiQuestionPack> _currentQuestionPacks = new List<UiQuestionPack>();
	private TreeNode<Category> _current;
	private LayoutGroup _currentGridLayout;
	Dictionary<TreeNode<Category>,UiSection> _uiMap = new Dictionary<TreeNode<Category>, UiSection>();
	#endregion

	private void Start ()
	{
		Instance = this;

		LoadNextSection(Content.Root);
	}

	public void AddNavigationStep(TreeNode<Category> previous)
	{
		var addedSection = Instantiate(SectionPrefab,_layout.transform);
		addedSection.Setup(previous);
		_uiMap.Add(previous,addedSection);
	}

	public void AddQuestionPack(QuestionPack pack)
	{
		var instance = Instantiate(QuestionPackPrefab, _layout.transform);
		instance.Setup(pack);
		_currentQuestionPacks.Add(instance);
	}

	private void ClearAllQuestionPack()
	{
		foreach (var pack in _currentQuestionPacks)
		{
			Destroy(pack.gameObject);
		}
		_currentQuestionPacks.Clear();
	}

	private void RemoveAllLowerChilds(TreeNode<Category> parent)
	{
		List<TreeNode<Category>> toDelete = new List<TreeNode<Category>>();
		int fromLevel = parent.Level;
		foreach (var section in _uiMap)
		{
			if (section.Key.Level > fromLevel)
			{
				toDelete.Add(section.Key);
			}
		}
		foreach (var entry in toDelete)
		{
			var uiObject = _uiMap[entry];
			Destroy(uiObject.gameObject);
			_uiMap.Remove(entry);
		}	
	}

	public void LoadPreviousSection(TreeNode<Category> previous)
	{
		//check if the section we clicked is latest
		if (previous.Level + 1 == _uiMap.Count) return;
		
		if(_currentGridLayout)Destroy(_currentGridLayout.gameObject);
		ClearAllQuestionPack();
		RemoveAllLowerChilds(previous);
		
		_current = previous;
		_currentGridLayout = Instantiate(_layoutGridPrefab,_layout.transform);
		foreach (var c in previous.Childrens)
		{
			var entry =  Instantiate(NextPrefab,_currentGridLayout.transform);
			entry.Setup(c);
		}
	}

	public void LoadNextSection(TreeNode<Category> next)
	{
		if (_current == next) return;

		if(_currentGridLayout)Destroy(_currentGridLayout.gameObject);
		ClearAllQuestionPack();
		AddNavigationStep(next);

		//load question pack or next
		if (next.Data.QuestionPack != null)
		{
			AddQuestionPack(next.Data.QuestionPack);
		}
		else
		{
			_current = next;
			_currentGridLayout = Instantiate(_layoutGridPrefab,_layout.transform);
			foreach (var c in next.Childrens)
			{
				var entry =  Instantiate(NextPrefab,_currentGridLayout.transform);
				entry.Setup(c);
			}
		}
	}
}
