using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HubContentLoader : MonoBehaviour
{
	#region FIELDS

	private const float TweenTime = 0.33f;
	public static HubContentLoader Instance;
	
	[SerializeField] private LocalPackDatabase _localDatabase;

	[SerializeField] private LayoutGroup _categoriesParent;
	[SerializeField] private LayoutGroup _subCategoriesParent;
	//PREFABS
	[SerializeField] private UiCategory _categoryPrefab;
	[SerializeField] private UiSubCategory _subCategoryPrefab;
	//CACHED
	private List<UiCategory> _categories = new List<UiCategory>();
	
	#endregion

	private void Awake()
	{
		Instance = this;
		//PRELOAD SECTIONS
		DisplayCategories(_localDatabase.Pack);
	}

	private void OnDestroy()
	{
		Instance = null;
	}

	private void DisplayCategories(Pack pack)
	{
		foreach (var category in pack.Categories)
		{
			AddCategoryButton(category);
		}

		TweenCategories(true);
	}

	#region Ui

	private void AddCategoryButton(Category category)
	{
		var instance = Instantiate(_categoryPrefab, _categoriesParent.transform);
		//configure it
		instance.Setup(category);
		//cache it
		_categories.Add(instance);
		//hide it
		instance.transform.ChangeLocalScaleY(0);
	}


	public void SelectCategory(Category category)
	{
		TweenCategories(false);
		//fade first group
		//show selected category
		//load subcategory
	}

	public void SelectSubCategory(SubCategory category)
	{
		//fade first group
		//show selected category
		//load subcategory
	}
	#region Tweens

	public void TweenCategories(bool visible)
	{
		float endValue = visible ? 1 : 0;
		foreach (var category in _categories)
		{
			category.transform.DOScaleY(endValue, TweenTime);
		}
	}

	#endregion
	#endregion
}
