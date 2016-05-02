using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public Transform mainMenu, optionsMenu;

	public void LoadScene(string name){
		SceneManager.LoadScene(name);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void OptionsMenu(bool clicked){
		if (clicked == true) {
			optionsMenu.gameObject.SetActive (clicked);
			mainMenu.gameObject.SetActive (false);
		} else {
			optionsMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(true);
		}
	}
}
