using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
	public List<GameObject> ammoTypes = new List<GameObject>();
	Stopwatch shotDelay;
	CharacterController ship;
	float motionInput;
	Vector3 distance = Vector3.zero;
	
	static int playerAmmo = 0; //0 = default, 1 = bar, 2 = bomb
	
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
			if ( transform.rotation.z != 0 )
				motionInput = -motionInput;
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
			distance = new Vector3(distanceTouch.x, transform.position.y, transform.position.z);
			move = shoot = teleport = true;
		}
		
		if ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(1) || Input.touchCount >= 3 )
			SetAmmo(0);
		
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
			newPosition.x = ( ( newPosition.x - (Screen.width/2) ) / ( Screen.width/2 ) ) / transform.localScale.x*3;
			newPosition.y = transform.position.y;
			transform.position = newPosition;
		}
	}
	
	void Shoot()
	{
		if ( shotDelay.IsRunning )
			if ( shotDelay.ElapsedMilliseconds < 100 || ( shotDelay.ElapsedMilliseconds < 1000 && playerAmmo == 2) )
				return;
		
		var ammo = ammoTypes[playerAmmo];

		GameObject shot = Instantiate(ammo, transform.position, Quaternion.identity);
		
		Vector3 dir = Vector3.up;
		if ( transform.rotation.z != 0 )
			dir = Vector3.down;

		shot.GetComponent<Rigidbody>().AddForce(dir * 1500);
		shotDelay = Stopwatch.StartNew();
		Destroy(shot, 2);
	}
	
	public static void SetAmmo(int idx)
	{
		playerAmmo = idx;
	}
}
