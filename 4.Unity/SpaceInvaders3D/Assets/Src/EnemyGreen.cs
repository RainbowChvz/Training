using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Enemy
{
	readonly int enemyHealth = 1;
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
		if ( HasCollision ( collideWith ) )
			remainingHealth--;
		
		if ( remainingHealth <= 0 )
			Destroy( gameObject );
	}
}