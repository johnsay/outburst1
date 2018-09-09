
using TreeView;
using UnityEngine;
using UnityEngine.UI;

public class UiChildSection : MonoBehaviour
{
	[SerializeField] private LayoutGroup _layout;
	private TreeNode<Category> _parent;

	public void Setup(TreeNode<Category> parent)
	{
		_parent = parent;
		foreach (var child in _parent.Childrens)
		{
			
		}
	}

	
}
