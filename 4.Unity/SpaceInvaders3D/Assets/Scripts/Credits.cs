using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
	public Button buttonBack;
	public Text credits;
	
	void Start()
	{
		buttonBack.onClick.AddListener(onBackButtonClick);
		
		credits.text = Application.productName + "\n"
						+ "Version: " + Application.version + "\n"
						+ "\n"
						+ credits.text;
	}
	
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
			SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
		
		float dirInput = Input.GetAxis(GameCore.STR_AXIS_DIRECTION_Y);
		if ( dirInput == 0 )
			return;
		
		Vector3 creditsPosition;
		if ( dirInput > 0 )
			creditsPosition = new Vector3( credits.transform.position.x, credits.transform.position.y + 3.5f, credits.transform.position.z );
		else
			creditsPosition = new Vector3( credits.transform.position.x, credits.transform.position.y - 3.5f, credits.transform.position.z );
		
		credits.transform.position = creditsPosition;
    }
	
	void onBackButtonClick()
	{
		SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
	}
}
