using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
	// CONSTANTS
	public const string	STR_GAMEOBJ_NAME_HERO		= "Player";
	public const string	STR_GAMEOBJ_NAME_ENEMY		= "Enemy";
	public const string	STR_GAMEOBJ_TAG_AMMO		= "Ammo";
	public const string	STR_GAMEOBJ_TAG_SPECIAL2	= "Bomb";
	
	public const string	STR_AXIS_DIRECTION_X		= "Horizontal";
	public const string	STR_AXIS_DIRECTION_Y		= "Vertical";
	
	public const string	STR_SCENE_SPLASH			= "Splash";
	public const string	STR_SCENE_TITLE				= "Title";
	public const string	STR_SCENE_GAMEPLAY			= "Gameplay";
	public const string	STR_SCENE_CREDITS			= "Credits";
	public const string	STR_SCENE_END				= "End";
	public const string	STR_SCENE_PAUSE				= "Pause";
	public const string	STR_SCENE_EXITCONFIRM		= "ExitConfirmation";
	public const string	STR_SCENE_HELP				= "Help";
	
	public const string	STR_LEVELMETADATA_FILENAME	= "Level";
	public const string	STR_LEVELMETADATA_MENUENTRY	= "Space Invaders/Level";
	
	public const int	INT_ENEMY_GREEN_1HP			= 1;
	public const int	INT_ENEMY_BLUE_2HP			= 2;
	public const int	INT_ENEMY_RED_3HP			= 3;
	
	public const int	INT_SCORE_POINTS_PER_HP		= 10;
	public const int	INT_SCORE_MULTIPLIER		= 3;
	public const int	INT_SCORE_MULTIPLIER_OFFSET	= 15;
	// CONSTANTS end

	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> players = new List<GameObject>();
	public Button buttonPause;
	public Text textScore, textMultiplier;
	
	public static bool scoreRefresh;	
	
	GameObject[] enemiesArray;
	LevelMetaData levelValues;

	static System.Random RNG = null;
	static bool gamePaused, enemiesHidden;
	static int score, highScore, prevHighScore;
	static string strMultiplier;
	
	void Start()
	{
		buttonPause.onClick.AddListener(OnPauseButtonClick);
		
		if ( enemiesArray == null )
		{
			LoadPlayer();
			AddScore(0);
			Player.SetAmmo(0);
			RefreshScoreUI();
			UpdatePrevHighScore();
			SetLevel(Title.titleSelection);
			LoadEnemies();
		}
	}
	
	void Update()
	{
		if ( Input.GetKeyDown(KeyCode.Escape) )
			PauseGame();
		
		if ( !IsPaused() && enemiesHidden)
			HideEnemies( false );
		
		if ( scoreRefresh )
		{
			RefreshScoreUI();
			if ( GameObject.FindWithTag( STR_GAMEOBJ_NAME_ENEMY ) == null )
				SceneManager.LoadScene( GameCore.STR_SCENE_END, LoadSceneMode.Single );
		}
	}
    
	void OnApplicationFocus(bool hasFocus)
    {
        if ( !hasFocus )
			if ( !IsPaused() )
				PauseGame();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if ( !IsPaused() )
			PauseGame();
    }
	
	void OnPauseButtonClick()
	{
		PauseGame();
	}

	void LoadPlayer()
	{
		Instantiate(players[0], players[0].transform.position, players[0].transform.rotation);
	}
	
	void LoadEnemies()
	{
		int currentEnemyCoordIndex = 0;
		enemiesArray = new GameObject[levelValues.enemyAmount];

		for (int i = 0; i < levelValues.enemyAmount; i++)
		{
			var prefabObject = GetRandomEnemy();
			enemiesArray[i] = Instantiate(prefabObject, levelValues.enemyLayout[currentEnemyCoordIndex], prefabObject.transform.rotation);
			// enemiesArray[i].name = levelValues.enemyPrefab + i+1;
			enemiesArray[i].name = enemiesArray[i].name.Substring( 0, enemiesArray[i].name.IndexOf( "(" ) ) + i;
			currentEnemyCoordIndex = (currentEnemyCoordIndex + 1) % levelValues.enemyLayout.Length;
		}
	}
	
	void HideEnemies(bool hidden)
	{
		enemiesHidden = hidden;
		
		if ( enemiesArray == null )
			return;

		for (int i = 0; i < enemiesArray.Length; i++)
			if (enemiesArray[i] != null)
				enemiesArray[i].SetActive( !hidden );
	}
	
	void PauseGame()
	{
		if ( IsPaused() )
			return;

		gamePaused = true;
		HideEnemies( true );
		SceneManager.LoadScene( GameCore.STR_SCENE_PAUSE, LoadSceneMode.Additive );
	}

	public static bool IsPaused()
	{
		return gamePaused;
	}
	
	GameObject GetRandomEnemy()
	{
		return enemies[GetRandomNumber( 0, 3 )];
	}
	
	public static void OnASceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if ( scene.name == GameCore.STR_SCENE_PAUSE )
		{
			gamePaused = true;
			SceneManager.SetActiveScene( scene );
		}
	}
	
	public static void OnASceneUnloaded()
	{
		gamePaused = false;
		if ( SceneManager.GetSceneByName( GameCore.STR_SCENE_GAMEPLAY ).isLoaded )
			SceneManager.SetActiveScene( SceneManager.GetSceneByName( GameCore.STR_SCENE_GAMEPLAY ) );
	}
	
	void SetLevel(int idx)
	{
		levelValues = Resources.Load<LevelMetaData>(STR_LEVELMETADATA_FILENAME + "_" + idx);
	}
	
	public static int GetPrevHighScore() { return prevHighScore; }
	public static int GetHighScore() { return highScore; }
	public static int GetScore() { return score; }
	
	static void UpdatePrevHighScore() { prevHighScore = highScore; }
	public static void SetHighScore( int hs ) { highScore = hs; }
	public static void AddScore( int s, bool multiplierKill = false )
	{
		strMultiplier = "";
		if ( s <= 0 )
		{
			score = 0;
			return;
		}
		
		scoreRefresh = true;
		
		if ( multiplierKill )
		{
			s *= INT_SCORE_MULTIPLIER;
			strMultiplier = "x" + INT_SCORE_MULTIPLIER;
		}
		
		score += s;
		if ( score > highScore )
			SetHighScore( score );
	}
	
	void RefreshScoreUI()
	{
		scoreRefresh = false;
		textScore.text = "Score: " + GetScore();
		textMultiplier.text = strMultiplier;
	}
	
	public static int GetRandomNumber( int min, int max )
	{
		if ( RNG == null )
			RNG = new System.Random();
		
		return RNG.Next( min , max );
	}
}
