
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HugContentLoader2 : MonoBehaviour 
{
    #region FIELDS

    public static HugContentLoader2 Instance;
    
    [SerializeField] private LocalPackDatabase _localDatabase;
    
    [SerializeField] private LayoutGroup _selectedGroup;
    [SerializeField] private LayoutGroup _categoriesParent;
    [SerializeField] private LayoutGroup _subCategoriesParent;
    //PREFABS
    [SerializeField] private UiSelectedSection _selectedPrefab;
    [SerializeField] private UiCategory _categoryPrefab;
    [SerializeField] private UiSubCategory _subCategoryPrefab;
    //CACHED
    private List<UiCategory> _categories = new List<UiCategory>();
    private List<UiSubCategory> _subCategories = new List<UiSubCategory>();
    private List< UiSelectedSection> _selections = new List<UiSelectedSection>();
    #endregion
    //cache current pack
    //list of previousSelection
    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        PreloadPrefabs();
        SelectPack(_localDatabase.Pack);
    }
    
    private void PreloadPrefabs()
    {
        for (int i = 0; i < 10; i++)
        {
            var instance = Instantiate(_selectedPrefab, _selectedGroup.transform);
            instance.gameObject.SetActive(false);
            _selections.Add(instance);
        }
    }


    private void LoadSelections(string[] selected)
    {
        for (int i = 0; i < _selections.Count; i++)
        {
            if (i < selected.Length)
            {
                _selections[i].Setup(selected[i]);
                _selections[i].Show();
            }
            else
            {
                _selections[i].Hide();
            }
        }
    }

    private void LoadCategories(Category[] categories)
    {

        foreach (var category in categories)
        {
            var instance = Instantiate(_categoryPrefab, _categoriesParent.transform);
            //configure it
            instance.Setup(category);
            //cache it
            _categories.Add(instance);
            //hide it
            instance.transform.ChangeLocalScaleY(0);
        }
    }

    private void DisableCategories()
    {
        foreach (var category in _categories)
        {
            Destroy(category.gameObject);
        }
        _categories.Clear();
    }

    private void LoadSubCategories(SubCategory[] subcategories)
    {
        DisableCategories();
        foreach (var subCategory in subcategories)
        {
            var instance = Instantiate(_subCategoryPrefab, _subCategoriesParent.transform);
            //configure it
            instance.Setup(subCategory);
            //cache it
            _subCategories.Add(instance);
            //hide it
            instance.transform.ChangeLocalScaleY(0);
        }
    }

    //receive inputs from ui button of categories
    //select pack
    private void SelectPack(Pack pack)
    {
        //hide selections
        LoadSelections(new string[0]);
        
        //load categories
        LoadCategories(pack.Categories);
    }
    //click on category

    public void SelectCategory(Category category)
    {
        LoadSelections(new string[1]{category.Description});
        
    }
    //click on subcategory
    
    //click on previous selection

}
