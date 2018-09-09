
using TreeView;
using UnityEngine;
using UnityEngine.UI;

public class UiNext : MonoBehaviour
{
	private TreeNode<Category> _node;
	[SerializeField] private Text _sectionName;
	
	
	public void Setup(TreeNode<Category> node)
	{
		_node = node;
		if( node!= null)
			_sectionName.text = node.Data.CategoryName;
	}

	public void OnUse()
	{
		UiHierarchy.Instance.LoadNextSection(_node);
	}
}
