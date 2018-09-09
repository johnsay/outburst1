using TreeView;
using UnityEngine;

[CreateAssetMenu]
public class ContentTree : ScriptableObject
{
    public TreeNode<Category> Root = new TreeNode<Category>(null);
    public QuestionPack[] QuestionPacks;

    private void OnEnable()
    {
        Root = PlaceholderContent.Generate();
    } 
}
  