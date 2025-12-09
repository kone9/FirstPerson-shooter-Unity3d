using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		float fps = 1.0f / deltaTime;

		// Estilo
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = 20;

		// Colores segun FPS
		if (fps < 20)
			style.normal.textColor = Color.red;        // Bajo
		else if (fps < 60)
			style.normal.textColor = Color.yellow;     // Medio
		else
			style.normal.textColor = Color.green;      // Alto

		// Posición: arriba a la derecha
		Rect rect = new Rect(0, 0, Screen.width - 10, 30);

		GUI.Label(rect, fps.ToString("F0") + " FPS", style);
	}
}
