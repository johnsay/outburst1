using UnityEngine;

[CreateAssetMenu(fileName = FileName, menuName = FileName, order = 1)]
public class LocalPackDatabase : ScriptableObject
{
    private const string FileName = "PackDb";
    public Pack Pack;
}
