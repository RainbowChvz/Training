using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
	public GameObject buttonBack;
	public Text credits;
	
	void Start()
	{
		Button butt = buttonBack.GetComponent<Button>();
		butt.onClick.AddListener(onBackButtonClick);
		
		credits.text = Application.productName + "\n"
						+ "Version: " + Application.version + "\n"
						+ "\n"
						+ credits.text;
	}
	
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
			SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
    }
	
	void onBackButtonClick()
	{
		SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
	}
}
