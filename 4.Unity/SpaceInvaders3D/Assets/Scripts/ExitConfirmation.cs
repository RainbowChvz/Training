using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitConfirmation : MonoBehaviour
{
	public GameObject buttonNo, buttonYes;
	
	enum ButtonIndex
	{
		IDX_BUTTON_NO,
		IDX_BUTTON_YES
	};
	
	void Start()
	{
		Button btn0 = buttonNo.GetComponent<Button>();
		Button btn1 = buttonYes.GetComponent<Button>();
		
		btn0.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_NO);});
		btn1.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_YES);});
	}
	
	void Update()
	{
		if ( Input.GetKeyDown(KeyCode.Escape) )
			SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
	}
	
	void OnButtonClick(ButtonIndex i)
	{
		switch ( i )
		{
			case ButtonIndex.IDX_BUTTON_NO:
				SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
				break;
			case ButtonIndex.IDX_BUTTON_YES:
				Application.Quit();
				break;
		}
	}
}
