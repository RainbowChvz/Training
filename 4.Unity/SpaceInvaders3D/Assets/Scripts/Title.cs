using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	public GameObject buttonLevel0, buttonLevel1, buttonAbout, buttonExit;
	enum ButtonIndex
	{
		IDX_BUTTON_LEVEL0,
		IDX_BUTTON_LEVEL1,
		IDX_BUTTON_ABOUT,
		IDX_BUTTON_EXIT
	};
	
	public static int titleSelection;
	
	void Start () {
		Button btn0 = buttonLevel0.GetComponent<Button>();
		Button btn1 = buttonLevel1.GetComponent<Button>();
		Button btn2 = buttonAbout.GetComponent<Button>();
		Button btn3 = buttonExit.GetComponent<Button>();
		
		btn0.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_LEVEL0);});
		btn1.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_LEVEL1);});
		btn2.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_ABOUT);});
		btn3.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_EXIT);});
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
			case ButtonIndex.IDX_BUTTON_EXIT:
				nextScene = GameCore.STR_SCENE_EXITCONFIRM;
				break;
		}
		
		if ( nextScene != null )
			SceneManager.LoadScene( nextScene, LoadSceneMode.Single );
	}
}
