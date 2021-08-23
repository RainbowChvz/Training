using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	public const string	STR_SCENE_PAUSE				= "5_Pause";
	public const string	STR_SCENE_EXITCONFIRM		= "6_ExitConfirmation";
	
	public const string	STR_LEVELMETADATA_FILENAME	= "Level";
	public const string	STR_LEVELMETADATA_MENUENTRY	= "Space Invaders/Level";
	
	public const int	INT_ENEMY_GREEN_1HP			= 1;
	public const int	INT_ENEMY_BLUE_2HP			= 2;
	public const int	INT_ENEMY_RED_3HP			= 3;
	public const int	INT_SCORE_POINTS_PER_HP		= 10;
	// CONSTANTS end

	public GameObject[] enemies = new GameObject[3];
	GameObject[] enemiesArray;
	public GameObject buttonPause, textScore;
	public static bool scoreRefresh;
	
	LevelMetaData levelValues;
	
	System.Random RNG = null;
	static bool gamePaused, enemiesHidden;
	static int score, highScore, prevHighScore;
	
	void Start()
	{
		Button btn0 = buttonPause.GetComponent<Button>();
		btn0.onClick.AddListener(OnPauseButtonClick);
		
		if ( enemiesArray == null )
		{
			AddScore(-1);
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
	
	void OnPauseButtonClick()
	{
		PauseGame();
	}

	void LoadEnemies()
	{
		int currentEnemyCoordIndex = 0;
		enemiesArray = new GameObject[levelValues.enemyAmount];

		for (int i = 0; i < levelValues.enemyAmount; i++)
		{
			enemiesArray[i] = Instantiate(GetRandomEnemy(), levelValues.enemyLayout[currentEnemyCoordIndex], Quaternion.identity);
			// enemiesArray[i].name = levelValues.enemyPrefab + i+1;
			enemiesArray[i].name = GameCore.STR_GAMEOBJ_NAME_ENEMY + i+1;
			currentEnemyCoordIndex = (currentEnemyCoordIndex + 1) % levelValues.enemyLayout.Length;
		}
	}
	
	void HideEnemies(bool hidden)
	{
		enemiesHidden = hidden;
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
		if ( RNG == null )
			RNG = new System.Random();
		
		int randomEnemy = RNG.Next( 0, 3 );
		return enemies[randomEnemy];
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
	public static void AddScore( int s )
	{
		if ( s < 0 )
		{
			score = 0;
			return;
		}
		
		score += s;
		if ( score > highScore )
			SetHighScore( score );
	}
	
	void RefreshScoreUI()
	{
		scoreRefresh = false;
		Text scoreBuffer = textScore.GetComponent<Text>();
		scoreBuffer.text = "Score: " + GetScore();
	}
}
