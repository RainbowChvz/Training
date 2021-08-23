using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject Bullet1;
	Stopwatch shotDelay;
	
	CharacterController ship;
	float motionInput;
	Vector3 distance = Vector3.zero;
	
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<CharacterController>();
		
		shotDelay = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
		if ( GameCore.IsPaused() )
			return;
		
		bool move = false, shoot = false, teleport = false;

		motionInput = Input.GetAxis( GameCore.STR_AXIS_DIRECTION_X );
		if ( motionInput != 0 )
		{
			distance = new Vector3( motionInput, 0, 0 );
			move = true;
		}
		
		motionInput = Input.GetAxis(GameCore.STR_AXIS_DIRECTION_Y);
		if ( motionInput > 0 )
			shoot = true;
		
		if ( Input.GetMouseButton(0) )
		{
			distance = Input.mousePosition;
			move = shoot = teleport = true;
		}
		
		if ( Input.touchCount > 0 )
		{
			Vector2 distanceTouch = Input.GetTouch( Input.touchCount - 1 ).position;
			distance = new Vector3(distanceTouch.x, distanceTouch.y, 0.0f);
			move = shoot = teleport = true;
		}
		
		if ( teleport && distance.y > Screen.height/4)
			return;
		
		if ( move )
			MovePlayer( distance, teleport );
		
		if ( shoot )
			Shoot();
    }
	
	void MovePlayer ( Vector3 newPosition, bool immediate = false )
	{
		if ( !immediate )
		{
			newPosition = transform.TransformDirection(newPosition);
			ship.Move(newPosition * Time.deltaTime * 35);
		}
		else
		{
			newPosition.x = ( ( newPosition.x - (Screen.width/2) ) / ( Screen.width/2 ) ) / transform.localScale.x*4;
			newPosition.y = 0.0f;
			transform.position = newPosition;
		}
	}
	
	void Shoot()
	{
		if ( shotDelay.IsRunning && shotDelay.ElapsedMilliseconds < 100 )
			return;

		GameObject shot = Instantiate(Bullet1, transform.position, Quaternion.identity);
		shot.GetComponent<Rigidbody>().AddForce(Vector3.up * 1500);
		shotDelay = Stopwatch.StartNew();
		Destroy(shot, 2);
	}
}
