
using UnityEngine;
using UnityEngine.UI;

public class UiQuestionPack : MonoBehaviour
{
    private QuestionPack _questionPack;
    [SerializeField] private Text _sectionName;
    [SerializeField] private Text _description;
	
    public void Setup(QuestionPack questionPack)
    {
        _questionPack = questionPack;
        if (questionPack != null)
        {
            _sectionName.text = questionPack.Title;
            _description.text = questionPack.Description;
        }
    }

    public void OnUse()
    {
        //UiHierarchy.Instance.LoadPreviousSection(_node);
    }
}
