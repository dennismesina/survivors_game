using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IdleScript : MonoBehaviour {

	public Text Idle_count_text;

	public int Idle;
	public int Total;

	public void display(){
		Idle_count_text.text = "Survivors Idle: " + Idle + " of " + Total;
	}

	public int get_idle(){
		return Idle;
	}

	public void set_idle(int I){
		Idle = I;
	}

	public int get_total(){
		return Total;
	}

	public void set_total(int T){
		Total = T;
	}
		
}
