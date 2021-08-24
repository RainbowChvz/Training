using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
	public Button back;
	
	void Start()
	{
		back.onClick.AddListener( OnBackClicked );
	}
	
	void Update()
	{
		if ( Input.GetKeyDown(KeyCode.Escape) )
			SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
	}

	void OnBackClicked()
	{
		SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
	}
}
