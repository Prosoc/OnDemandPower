using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {

    int sceneNumber = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Startgame()
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
