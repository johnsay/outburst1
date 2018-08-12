using UnityEngine;

[CreateAssetMenu(fileName = FileName, menuName = FileName, order = 1)]
public class QuestionsPack : ScriptableObject {

    private const string FileName = "QuestionsPack";
    public QuestionGroup Questions;
}
