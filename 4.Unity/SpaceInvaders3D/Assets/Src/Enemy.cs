using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	CharacterController enemy;
	// Vector3 distance = Vector3.zero;
	Vector3 distance = new Vector3(0, -1, 0);
	Stopwatch collisionCooldown = new Stopwatch();
	
    // Start is called before the first frame update
    protected void Start()
    {
        enemy = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		distance = transform.TransformDirection(distance);
		enemy.Move(distance * Time.deltaTime);
    }
	
	protected bool HasCollision ( Collider collideWith )
	{
		if ( collideWith.gameObject.CompareTag("Bullet1") )
		{
			Destroy( collideWith.gameObject );
			collisionCooldown = Stopwatch.StartNew();
			return true;
		}
		
		if ( collideWith.gameObject.name == "Player" )
			Destroy( collideWith.gameObject );

		return false;		
	}
	
	protected bool IsCoolingDown()
	{
		if ( collisionCooldown.IsRunning && collisionCooldown.ElapsedMilliseconds < 250 )
			return true;
		
		collisionCooldown.Reset();
		return false;
	}
}
