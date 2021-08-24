using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
	CharacterController enemy;
	Renderer enemyRenderer;
	
	int enemyHealth, enemyRemainingHealth;
	Material enemyMaterial;
	
	// Vector3 movement = Vector3.zero;
	// Vector3 movement = new Vector3(0, -1, 0);
	Vector3 movement;
	Stopwatch collisionCooldown = new Stopwatch();
	
	const float enemySpeed = 0.5f;
	
    // Start is called before the first frame update
    protected void Start()
    {
        enemy = GetComponent<CharacterController>();
        enemyRenderer = GetComponent<Renderer>();
		enemyHealth = enemyRemainingHealth = GetHealth();
    }

    // Update is called once per frame
    protected void Update()
    {
		movement = new Vector3(Mathf.Sin(Time.time)*2, -1, 0);
		movement = transform.TransformDirection(movement);
		enemy.Move(movement * Time.deltaTime * enemySpeed);
    }
	
	void OnTriggerEnter( Collider collideWith )
	{		
		if ( collideWith.gameObject.CompareTag( GameCore.STR_GAMEOBJ_TAG_AMMO ) )
		{
			if ( IsCoolingDown () )
				return;
			collisionCooldown = Stopwatch.StartNew();
			
			if ( collideWith.gameObject.name.Contains( GameCore.STR_GAMEOBJ_TAG_SPECIAL2 ) )
				enemyRemainingHealth = 0;
			else
			{
				enemyRemainingHealth--;
				Destroy( collideWith.gameObject );
			}
		}
		
		if ( collideWith.gameObject.name == GameCore.STR_GAMEOBJ_NAME_HERO )
		{
			Destroy( collideWith.gameObject );
			SceneManager.LoadScene( GameCore.STR_SCENE_END, LoadSceneMode.Single );
			return;
		}
		
		if ( enemyRemainingHealth <= 0 )
		{
			if ( GameCore.GetRandomNumber( 0, 6 ) == 5 ) // 20% chance that the enemy drops a special ammo.
				Player.SetAmmo( GameCore.GetRandomNumber( 1, 3 ));
			GameCore.AddScore( enemyHealth * GameCore.INT_SCORE_POINTS_PER_HP, transform.position.y > 10 );
			
			Destroy( gameObject );
		}
	}
	
	protected bool IsCoolingDown()
	{
		if ( collisionCooldown.IsRunning && collisionCooldown.ElapsedMilliseconds < 125 )
			return true;
		
		collisionCooldown.Reset();
		return false;
	}
	
	int GetHealth()
	{
		string enemyColor = enemyRenderer.material.ToString();
		
		if ( enemyColor.StartsWith("Green") )
			return 1;
		else if ( enemyColor.StartsWith("Blue") )
			return 2;
		else if ( enemyColor.StartsWith("Red") )
			return 3;
		
		return 0;
	}
}
