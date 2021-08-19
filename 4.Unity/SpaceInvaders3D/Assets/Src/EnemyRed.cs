using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    readonly int enemyHealth = 3;
	int remainingHealth;
	
    // Start is called before the first frame update
    void Start()
    {
		remainingHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	void OnTriggerEnter( Collider collideWith )
	{
		Debug.Log( "Enemy RED! Remaining health: "+remainingHealth );
		if ( IsCoolingDown () )
			return;
		
		if ( HasCollision ( collideWith ) )
			remainingHealth--;
		
		if ( remainingHealth <= 0 )
			Destroy( gameObject );
	}
}
