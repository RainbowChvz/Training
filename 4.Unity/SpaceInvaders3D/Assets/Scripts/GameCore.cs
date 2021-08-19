using UnityEngine;

public class GameCore : MonoBehaviour
{

	public GameObject enemy;
	public LevelMetaData levelValues;
	
	void Start()
	{
		LoadEnemies();
	}

	void LoadEnemies()
	{
		int currentEnemyCoordIndex = 0;

		for (int i = 1; i <= levelValues.enemyAmount; i++)
		{
			GameObject currentEnemy = Instantiate(enemy, levelValues.enemyLayout[currentEnemyCoordIndex], Quaternion.identity);
			currentEnemy.name = levelValues.enemyPrefab + i;
			currentEnemyCoordIndex = (currentEnemyCoordIndex + 1) % levelValues.enemyLayout.Length;
		}
	}
}

