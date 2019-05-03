using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {
List<string> components = new List<string>();

	GameObject[] canvas;

	//Panels
	GameObject[] mainMenuPanel;
	GameObject[] button; 

	void Start () {
		CreateInitial();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateInitial()
	{
		components.Clear();
		components.AddRange(new string[]{ "Canvas"});
		Creador(ref canvas, "Canvas", 1, components);

		components.Clear();
		components.AddRange(new string[]{ "RectTransform", "CanvasRenderer","CanvasAsParent","Stretch"});
		Creador(ref mainMenuPanel, "MainPanel", 1, components);

		components.Clear();
		components.AddRange(new string[]{ "RectTransform", "CanvasRenderer", "Button","Image","MainMenuPanelAsParent","AlignButton"});
		Creador(ref button, "Button", 2, components);
		
		button[0].GetComponent<Button>().onClick.AddListener(Open_scene);
		button[1].GetComponent<Button>().onClick.AddListener(Quit_game);
	}

void Open_scene()
	{
		SceneManager.LoadScene(1);
	}
void Quit_game(){
		Debug.Log("asda");
		Application.Quit();
	}
//***********************************************************************************************************************
	//Function to give Parameters to Game Objects

	void Creador(ref GameObject[] goc, string name, int size, List<string> parameters){
		//Creates Game Objects
		goc = new GameObject[size];
		for(int i = 0; i < goc.Length; i++){
			goc[i] = new GameObject(name + i);
		}
//**************************************************
//Adds components to objects
		for(int i = 0; i < parameters.Count; i++){
			
			if(parameters[i] == "Button"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].AddComponent<Button>();					
				}
			}
			if(parameters[i] == "RectTransform"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].AddComponent<RectTransform>();
				}
			}
			if(parameters[i] == "Image"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].AddComponent<Image>();
				}
			}
			if(parameters[i] == "CanvasRenderer"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].AddComponent<CanvasRenderer>();					
				}
			}
			if(parameters[i] == "GraphicRaycaster"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].AddComponent<GraphicRaycaster>();
				}
			}
			if(parameters[i] == "Canvas"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].AddComponent<Canvas>();
					goc[j].GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
					goc[j].AddComponent<CanvasScaler>();
					goc[j].AddComponent<GraphicRaycaster>();
					
				}
			}
			if(parameters[i] == "Text"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].AddComponent<Text>();

				}
			}	
//**************************************************
//Set Parents

			if(parameters[i] == "CanvasAsParent"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].transform.SetParent(canvas[0].transform);
				}
			}
			if(parameters[i] == "MainMenuPanelAsParent"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].transform.SetParent(mainMenuPanel[0].transform);
				}
			}

			
			

//**************************************************
// Positioning
if(parameters[i] == "Stretch"){
				for(int j = 0; j < goc.Length; j++){     				  
      				goc[j].GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);                                          
      				goc[j].GetComponent<RectTransform>().anchorMax = new Vector2(1, 1); 
      				goc[j].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        			goc[j].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);				
				}
			}
if(parameters[i] == "AlignButton"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);									
					if (j == 0 ) {
						goc[j].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/7*2, Screen.width/7*2);	
						goc[j].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);                                          
						goc[j].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
						//goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
					}
					if(j == 1 ) {
						goc[j].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/12, Screen.width/12);	
						goc[j].GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);                                          
 						goc[j].GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
 						goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width/24, -Screen.width/24);
					}
										
				}

			}






}
}
}