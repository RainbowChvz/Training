using System;
using System.Collections;
using UnityEngine;

public class GameCore : MonoBehaviour
{
	// CONSTANTS
	public const string	STR_GAMEOBJ_NAME_HERO		= "Player";
	public const string	STR_GAMEOBJ_NAME_ENEMY		= "Enemy";
	public const string	STR_GAMEOBJ_TAG_AMMO		= "Bullet1";
	
	public const string	STR_AXIS_DIRECTION_X		= "Horizontal";
	public const string	STR_AXIS_DIRECTION_Y		= "Vertical";
	
	public const string	STR_SCENE_SPLASH			= "1_Splash";
	public const string	STR_SCENE_TITLE				= "2_Title";
	public const string	STR_SCENE_GAMEPLAY			= "3.1_Gameplay";
	public const string	STR_SCENE_CREDITS			= "3.2_Credits";
	public const string	STR_SCENE_END				= "4_End";
	
	public const string	STR_LEVELMETADATA_FILENAME	= "Level";
	public const string	STR_LEVELMETADATA_MENUENTRY	= "Space Invaders/Level";
	
	public const int	INT_ENEMY_GREEN_1HP			= 1;
	public const int	INT_ENEMY_BLUE_2HP			= 2;
	public const int	INT_ENEMY_RED_3HP			= 3;
	// CONSTANTS end

	public GameObject[] enemies = new GameObject[3];
	public LevelMetaData levelValues;
	
	System.Random RNG = null;
	
	void Start()
	{
		LoadEnemies();
	}

	void LoadEnemies()
	{
		int currentEnemyCoordIndex = 0;

		for (int i = 1; i <= levelValues.enemyAmount; i++)
		{
			GameObject currentEnemy = Instantiate(GetRandomEnemy(), levelValues.enemyLayout[currentEnemyCoordIndex], Quaternion.identity);
			// currentEnemy.name = levelValues.enemyPrefab + i;
			currentEnemy.name = GameCore.STR_GAMEOBJ_NAME_ENEMY + i;
			currentEnemyCoordIndex = (currentEnemyCoordIndex + 1) % levelValues.enemyLayout.Length;
		}
	}
	
	GameObject GetRandomEnemy()
	{
		if ( RNG == null )
			RNG = new System.Random();
		
		int randomEnemy = RNG.Next( 0, 3 );
		return enemies[randomEnemy];
	}
}

