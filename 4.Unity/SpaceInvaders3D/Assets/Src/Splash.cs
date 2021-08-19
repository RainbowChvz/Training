using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Time.realtimeSinceStartup > 5 )
			SceneManager.LoadScene( "SCR_Gameplay", LoadSceneMode.Single );
    }
}
