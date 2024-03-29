using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
	public GameObject buttonRetry, buttonTitleScreen, textScore, textHighScore;
	int increaseScoreText;
	Text scoreBuffer;
	
	enum ButtonIndex
	{
		IDX_BUTTON_RETRY,
		IDX_BUTTON_TITLE_SCREEN
	};
	
    void Start()
    {
        Button btn0 = buttonRetry.GetComponent<Button>();
		Button btn1 = buttonTitleScreen.GetComponent<Button>();
		
		btn0.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_RETRY);});
		btn1.onClick.AddListener(delegate{OnButtonClick(ButtonIndex.IDX_BUTTON_TITLE_SCREEN);});
		
		Text scoreBuffer = textHighScore.GetComponent<Text>();
		scoreBuffer.text = "High Score: " + GameCore.GetPrevHighScore();
		
    }
	
	void Update()
	{	
		if ( increaseScoreText < GameCore.GetScore() )
		{
			if ( !Input.GetMouseButton(0) && Input.touchCount <= 0 )
				increaseScoreText += GameCore.INT_SCORE_POINTS_PER_HP;
			else
				increaseScoreText = GameCore.GetScore();
				
			
			if ( increaseScoreText > GameCore.GetScore() )
				increaseScoreText = GameCore.GetScore();
			
			if ( increaseScoreText % 10 == 0 || increaseScoreText == GameCore.GetScore() )
			{	
				scoreBuffer = textScore.GetComponent<Text>();
				scoreBuffer.text = "Score: " + (increaseScoreText);
				
				if ( increaseScoreText > GameCore.GetPrevHighScore() )
				{
					scoreBuffer = textHighScore.GetComponent<Text>();
					scoreBuffer.text = "High Score: " + increaseScoreText + "   NEW!!!";
				}
			}
		}
	}

    void OnButtonClick(ButtonIndex i)
	{
		string nextScene = null;
		switch ( i )
		{
			case ButtonIndex.IDX_BUTTON_RETRY:
				nextScene = GameCore.STR_SCENE_GAMEPLAY;
				break;
			case ButtonIndex.IDX_BUTTON_TITLE_SCREEN:
				nextScene = GameCore.STR_SCENE_TITLE;
				break;
		}
		
		if ( nextScene != null )
			SceneManager.LoadScene( nextScene, LoadSceneMode.Single );
	}
}
