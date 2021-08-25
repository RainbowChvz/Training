using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	public Button buttonLevel0, buttonLevel1, buttonAbout, buttonExit, buttonHelp;
	enum ButtonIndex
	{
		IDX_BUTTON_LEVEL0,
		IDX_BUTTON_LEVEL1,
		IDX_BUTTON_ABOUT,
		IDX_BUTTON_EXIT,
		IDX_BUTTON_HELP
	};
	
	public static int titleSelection;
	
	void Start () {
		buttonLevel0.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_LEVEL0);});
		buttonLevel1.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_LEVEL1);});
		buttonAbout.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_ABOUT);});
		buttonHelp.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_HELP);});
		buttonExit.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_EXIT);});
	}
	
	void Update()
	{
		if ( Input.GetKeyDown(KeyCode.Escape) )
			SceneManager.LoadScene( GameCore.STR_SCENE_EXITCONFIRM, LoadSceneMode.Single );
	}

	void OnButtonClick(ButtonIndex i)
	{
		titleSelection = (int) i;
		string nextScene = null;
		switch ( i )
		{
			case ButtonIndex.IDX_BUTTON_LEVEL0:
			case ButtonIndex.IDX_BUTTON_LEVEL1:
				nextScene = GameCore.STR_SCENE_GAMEPLAY;
				break;
			case ButtonIndex.IDX_BUTTON_ABOUT:
				nextScene = GameCore.STR_SCENE_CREDITS;
				break;
			case ButtonIndex.IDX_BUTTON_HELP:
				nextScene = GameCore.STR_SCENE_HELP;
				break;
			case ButtonIndex.IDX_BUTTON_EXIT:
				nextScene = GameCore.STR_SCENE_EXITCONFIRM;
				break;
		}
		
		if ( nextScene != null )
			SceneManager.LoadScene( nextScene, LoadSceneMode.Single );
	}
}
