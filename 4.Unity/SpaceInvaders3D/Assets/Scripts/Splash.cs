using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
	float splashTime = 3.0f;
	
    // Start is called before the first frame update
    void Start()
    {
		Invoke("OnExitSplash", splashTime);
    }

    void OnExitSplash()
    {
        SceneManager.LoadScene( GameCore.STR_SCENE_TITLE, LoadSceneMode.Single );
    }
}
