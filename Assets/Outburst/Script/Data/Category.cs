using System;

[Serializable]
public class Category
{
    public string Name;
    public Category[] SubCategories;
    public QuestionGroup Questions;
}
