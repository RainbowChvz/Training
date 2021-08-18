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
		if (!Input.anyKey)
			return;

		motionInput = Input.GetAxis("Horizontal");
		if ( motionInput != 0 )
		{
			distance = new Vector3(motionInput, 0, 0);
			distance = transform.TransformDirection(distance);
			ship.Move(distance * Time.deltaTime * 35);
		}
		
		motionInput = Input.GetAxis("Vertical");
		if ( motionInput > 0 )
		// if ( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) )
		{
			if ( shotDelay.IsRunning && shotDelay.ElapsedMilliseconds < 250 )
				return;
			GameObject shot = Instantiate(Bullet1, transform.position, Quaternion.identity);
			shot.GetComponent<Rigidbody>().AddForce(Vector3.up * 1500);
			
			shotDelay = Stopwatch.StartNew();
			
			Destroy(shot, 1);
		}
		else
		{
			shotDelay.Reset();
		}
    }
}
