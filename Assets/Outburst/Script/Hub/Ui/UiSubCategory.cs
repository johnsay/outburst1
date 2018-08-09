using UnityEngine;
using UnityEngine.UI;

public class UiSubCategory : MonoBehaviour 
{
    #region FIELDS

    [SerializeField] private Text _description;
    //[SerializeField] private Image _icon;

    private SubCategory _category;

    #endregion

    public void Setup(SubCategory category)
    {
        _category = category;
        _description.text = _category.Description;
    }

    public void Select()
    {
        if (HubContentLoader.Instance)
        {
            HubContentLoader.Instance.SelectSubCategory(_category);
        }
    }
}
