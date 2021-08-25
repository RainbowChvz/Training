using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitConfirmation : MonoBehaviour
{
	public Button buttonNo, buttonYes;
	
	enum ButtonIndex
	{
		IDX_BUTTON_NO,
		IDX_BUTTON_YES
	};
	
	void Start()
	{
		buttonNo.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_NO);});
		buttonYes.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_YES);});
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
