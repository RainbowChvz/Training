using UnityEngine;

[CreateAssetMenu(fileName = GameCore.STR_LEVELMETADATA_FILENAME, menuName = GameCore.STR_LEVELMETADATA_MENUENTRY, order = 1)]
public class LevelMetaData : ScriptableObject
{
    public string enemyPrefab;

    public int enemyAmount;
    public Vector3[] enemyLayout;
}
