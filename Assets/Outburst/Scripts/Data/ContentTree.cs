using TreeView;
using UnityEngine;

[CreateAssetMenu]
public class ContentTree : ScriptableObject
{
    public TreeNode<Category> Root = new TreeNode<Category>(null);
    public PlaceholderContent.PlaceHolderCategory[] QuestionPacks;

    private void OnEnable()
    {
        Root = PlaceholderContent.Generate(this);
    } 
  
  
  
}
  