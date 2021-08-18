using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	
	CharacterController ship;
	float motionInput;
	Vector3 distance = Vector3.zero;
	
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<CharacterController>();
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
		{
			// TODO shooting
		}
    }
}
