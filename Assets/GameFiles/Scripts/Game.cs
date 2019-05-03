using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	List<string> components = new List<string>();

	GameObject[] canvas;

	//Panels
	GameObject[] gamePanel;
	GameObject[] hpPanel;
	GameObject[] img; 

	public float timer = 10f;
	[Range(0,100)]
	public float hpCoefficient_1 = 100f;
	[Range(0,100)]
	public float hpCoefficient_2 = 100f;

	GameObject[] timer_s;
	
	void Start () {
		CreateInitial();		
	}
	
	// Update is called once per frame
	void Update () {
		Timer();
		HPBar();
		}

//*********************************************************************************************************************************
//Function to Create Game objects	

	void CreateInitial(){
		components.Clear();
		components.AddRange(new string[]{ "Canvas"});
		Creador(ref canvas, "Canvas", 1, components);

		components.Clear();
		components.AddRange(new string[]{ "RectTransform", "CanvasRenderer","CanvasAsParent","Stretch"});
		Creador(ref gamePanel, "MainPanel", 1, components);		

		components.Clear();
		components.AddRange(new string[]{ "RectTransform", "CanvasRenderer","gamePanelAsParent","AlignLeftRightTop","hpPanelParam"});
		Creador(ref hpPanel, "hpPanel", 2, components);	

		components.Clear();
		components.AddRange(new string[]{ "RectTransform", "CanvasRenderer", "Image","hpPanelAsParent","AlignLeftRightTop","ImageParam"});
		Creador(ref img, "img", 2, components);

		components.Clear();
		components.AddRange(new string[]{ "RectTransform", "CanvasRenderer", "Text", "gamePanelAsParent", "AlignTop","Aligntext"});
		Creador(ref timer_s, "Timer", 1, components);
		}

		void Timer(){
		timer -= Time.deltaTime;
		timer_s[0].GetComponent<Text>().text = timer.ToString("00");
		if (timer == 0){
			timer = 0;
			}
		}	
		void HPBar(){
			img[0].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4*hpCoefficient_1/100, Screen.width/32);
			img[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/8*hpCoefficient_1/100, -Screen.width/64);

			img[1].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4*hpCoefficient_2/100, Screen.width/32);
			img[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width/8*hpCoefficient_2/100, -Screen.width/64);
		}


//***********************************************************************************************************************
//Function to give Parameters to Game Objects

	void Creador(ref GameObject[] goc, string name, int size, List<string> parameters){
		//Creates Game Objects
		goc = new GameObject[size];
		for(int i = 0; i < goc.Length; i++){
			goc[i] = new GameObject(name + i);
		}
//**********************************************************************************************************************************
//Adds components to objects
		for(int i = 0; i < parameters.Count; i++){

			if(parameters[i] == "Aligntext"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height/5, Screen.height/5);
					goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -Screen.height/10);
//**********************************************************************************************************************
					//ADIII POMENYAY FONT 
					Font abc = Resources.Load("Fonts/SaucerBB",typeof(Font)) as Font;
					//Debug.Log(abc);
					goc[j].GetComponent<Text>().font = abc;
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
//**********************************************************************************************************************************
//Set Parents

			if(parameters[i] == "CanvasAsParent"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].transform.SetParent(canvas[0].transform);
				}
			}
			if(parameters[i] == "gamePanelAsParent"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].transform.SetParent(gamePanel[0].transform);
				}
			}
			if(parameters[i] == "hpPanelAsParent"){
				for(int j = 0; j < goc.Length; j++){
					if (j == 0 ) {
					goc[j].transform.SetParent(hpPanel[0].transform);
					}
					if (j == 1 ) {
					goc[j].transform.SetParent(hpPanel[1].transform);
					}
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
if(parameters[i] == "AlignLeftRightTop"){
				for(int j = 0; j < goc.Length; j++){
					if (j == 0 ) {
					goc[j].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);                                          
					goc[j].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
					}
					if (j == 1 ) {
						goc[j].GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);                                          
						goc[j].GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
					}
				}
			}
if(parameters[i] == "AlignTop"){
				for(int j = 0; j < goc.Length; j++){
					goc[j].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);                                          
					goc[j].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
				}
			}
//**************************************************
//Parameters

	if(parameters[i] == "ImageParam"){
				for(int j = 0; j < goc.Length; j++){					
					//goc[j].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4*hpCoefficient/100, Screen.width/32);
					//goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/8, -Screen.width/64);
					//goc[j].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.width/24);
					//goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/6, -Screen.width/48);		       	                                      
				}
			}	
if(parameters[i] == "hpPanelParam"){
				for(int j = 0; j < goc.Length; j++){					
					goc[j].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4, Screen.width/32);
					if (j == 0 ) {
					goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/8, -Screen.width/64);
					}
					if (j == 1 ) {
					goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width/8, -Screen.width/64);
					}
					//goc[j].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.width/24);
					//goc[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/6, -Screen.width/48);		       	                                      
				}
			}	




	}
}
}