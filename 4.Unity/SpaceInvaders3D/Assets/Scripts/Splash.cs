using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
	[SerializeField]
	[Min(3)]
	int splashTime;
	
	[SerializeField]
	string nextScene = "2_Title";
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( Time.realtimeSinceStartup > splashTime )
			SceneManager.LoadScene( nextScene, LoadSceneMode.Single );
    }
}
