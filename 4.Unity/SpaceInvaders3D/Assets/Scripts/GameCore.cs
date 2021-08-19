using UnityEngine;

public class GameCore : MonoBehaviour
{
	// The GameObject to instantiate.
	public GameObject entityToSpawn;

	// An instance of the ScriptableObject defined above.
	public LevelMetaData levelValues;
	
	void Start()
	{
		SpawnEntities();
	}

	void SpawnEntities()
	{
		int currentSpawnPointIndex = 0;

		for (int i = 1; i <= levelValues.enemyAmount; i++)
		{
			// Creates an instance of the prefab at the current spawn point.
			GameObject currentEntity = Instantiate(entityToSpawn, levelValues.enemyLayout[currentSpawnPointIndex], Quaternion.identity);

			// Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
			currentEntity.name = levelValues.enemyPrefab + i;

			// Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
			currentSpawnPointIndex = (currentSpawnPointIndex + 1) % levelValues.enemyLayout.Length;
		}
	}
}
