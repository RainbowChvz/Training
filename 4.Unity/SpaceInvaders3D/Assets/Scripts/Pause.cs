using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	public GameObject buttonResume, buttonTitleScreen;
	enum ButtonIndex
	{
		IDX_BUTTON_RESUME,
		IDX_BUTTON_TITLE_SCREEN
	};
	
	void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
    void Start()
    {
        Button btn0 = buttonResume.GetComponent<Button>();
		Button btn1 = buttonTitleScreen.GetComponent<Button>();
		
		btn0.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_RESUME);});
		btn1.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_TITLE_SCREEN);});
    }
	
	void Update()
	{
		if ( Input.GetKeyDown(KeyCode.Escape) )
			ResumeGame();
	}

    void OnButtonClick(ButtonIndex i)
	{
		switch ( i )
		{
			case ButtonIndex.IDX_BUTTON_RESUME:
				ResumeGame();
				break;
			case ButtonIndex.IDX_BUTTON_TITLE_SCREEN:
				SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
				break;
		}
	}
	
	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		OnSceneUnloaded();
	}
	
	void OnSceneLoaded ( Scene scene, LoadSceneMode mode ) { GameCore.OnASceneLoaded( scene, mode ); }
	void OnSceneUnloaded() { GameCore.OnASceneUnloaded(); }
	
	void ResumeGame()
	{
		SceneManager.UnloadSceneAsync( GameCore.STR_SCENE_PAUSE );
	}
}
