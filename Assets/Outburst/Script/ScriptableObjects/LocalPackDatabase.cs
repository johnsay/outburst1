using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FileName, menuName = FileName, order = 1)]
public class LocalPackDatabase : ScriptableObject
{
    private const string FileName = "PackDb";
    public Category[] Categories;
    //packs: k12, custom_teacher
    //grade level: mostly age
    //category:math, science

    //group: small animals, diseases(separate SO)
}
