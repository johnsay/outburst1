using System.Collections.Generic;
using TreeView;
using UnityEngine;

public class PlaceholderContent 
{
	public static TreeNode<Category> Generate()
	{
		var root = new TreeNode<Category>(BuildNew("content pack"));

		string[] type1 = {"Kindergarden", "MiddleSchool", "High School"};
		string[] type2 = {"math", "sport", "music"};
		foreach (var entry in type1)
		{
			var cat = BuildNew(entry);
			var child = root.AddChild(cat);

			foreach (var matiere in type2)
			{
				var subcat = BuildNew(matiere);
				subcat.QuestionPack = GenerateQp();
				child.AddChild(subcat);
			}
		}

		return root;
	}
	
	private static Category BuildNew(string catName)
	{
		var cat = new Category();
		cat.CategoryName =catName;
		return cat;
	}

	private static QuestionPack GenerateQp()
	{
		QuestionPack pack = new QuestionPack();
		pack.Title = "Questions "+Random.Range(100, 3000);
		pack.Description = "some questions";
		List<Question> qcache = new List<Question>();
		for (int i = 0; i < 5; i++)
		{
			Question q1 = new Question();
			q1.Description = "question number: "+i;
			q1.Answers = new[] {"yes","no","maybe"};
			qcache.Add(q1);
		}

		pack.Content = qcache.ToArray();
        
		return pack;
	}
}
