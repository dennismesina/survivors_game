using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateSurvivor : MonoBehaviour {

	public Text Idle_count_text;
	private int Idle;
	private int TotalSurv;

	public Text Job_title_text;
	public Text Active_job_text;
	public Text Of_total_text;
	private int ActiveJob = 0;

	public Button minus;
	public Button plus;
	string Pressed = "";


//============================================================	
	// Get Idle and Total Numbers from IdleScript
//============================================================
	void Get_idle_n_total()
	{
		IdleScript sn = Idle_count_text.GetComponent<IdleScript> ();	// Allows the use of
		Idle = sn.get_idle ();											//	IdleScript Functions
		TotalSurv = sn.get_total ();
	}
//============================================================	
	// Set Idle and Total onto IdleScript
//============================================================
	void Set_idle_n_total()
	{
		IdleScript sn = Idle_count_text.GetComponent<IdleScript> ();
		sn.set_idle (Idle);
		sn.set_total ();
	}
//============================================================	
	/* Initialize functions if we want to add/subtract a survivor 
		then we just call the function */
//============================================================ 
	void Add_Survivors()
	{
		TotalSurv++;
		Idle++;
		Set_idle_n_total ();
	}
	void Sub_Survivors()
	{
		TotalSurv--;
		// Where do we pull the survivor from? 
			// 1 less idle/Scavenging/Exploring...?
		Set_idle_n_total ();
	}
//============================================================	
	// Start Function
//============================================================
	void Start () 
	{
		Button B = minus.GetComponent<Button> ();
		B.onClick.AddListener (SetMinus);
		B = plus.GetComponent<Button> ();
		B.onClick.AddListener (SetPlus);
	}
//============================================================	
	// Update Function
//============================================================
	void Update()
	{
		Get_idle_n_total ();
		Display_info ();
	}
//============================================================
	// Function that displays the data on the screen
//============================================================
	void Display_info()
	{
		IdleScript sn = Idle_count_text.GetComponent<IdleScript> ();
		sn.display ();
		Job_title_text.text.ToString ();
		Active_job_text.text = ActiveJob.ToString();
		Of_total_text.text = "of " + TotalSurv;
	}
//============================================================
	// Checks what button is pressed and calls the action function
//============================================================
	void SetMinus()
	{
		Debug.Log ("Button is Minus");
		Pressed = "M";
		TaskOnClick ();
	}
	void SetPlus()
	{
		Debug.Log ("Button is Plus");
		Pressed = "P";
		TaskOnClick ();
	}
//============================================================	
	// Function to perform when the user clicks the button
//============================================================
	void TaskOnClick()
	{
		if (Check (Pressed))
			Set_job(Pressed);
	}
//	============================================================
/* 	Check and Set Function
	If there is no more idled then you can't add any more survivors to do a job
	If there is the max idled then you can't subtract survivors from a job */
//============================================================
	bool Check(string B)
	{
		if ((B == "P") && ((Idle == 0) || (ActiveJob == TotalSurv)) || (B == "M") && ((Idle == TotalSurv) || (ActiveJob == 0)))
			return false;
		return true;
	}
	void Set_job(string B)
	{
		if (B == "P") {
			ActiveJob++;
			// Call the action function for survivor to conduct job
			Idle--;
			Set_idle_n_total ();
		} else if(B == "M") { 
			ActiveJob--;
			// Call the action function for survivor to idle
			Idle++;
			Set_idle_n_total ();
		}
	}
}