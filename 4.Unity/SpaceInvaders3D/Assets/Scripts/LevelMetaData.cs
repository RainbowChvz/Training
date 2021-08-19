using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Space Invaders/Level", order = 1)]
public class LevelMetaData : ScriptableObject
{
    public string enemyPrefab;

    public int enemyAmount;
    public Vector3[] enemyLayout;
}
