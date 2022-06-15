using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMovement : MonoBehaviour
{
    public bool lockMouse = true; //Controla se o cursor do mouse é exibido
	public float sensitivity = 10.0f; //Controla a sensibilidade do mouse
	public GameObject rightHand;
	public bool lockCamera = false;

	private float mouseX = 0.0f, mouseY = 0.0f; //Variáveis que controla a rotação do mouse

	void Start()
	{
        if(CurrentPlatformManager.instance.currentPlatform != RuntimePlatform.WindowsEditor &&
		   CurrentPlatformManager.instance.currentPlatform != RuntimePlatform.WindowsPlayer)
        {
            GetComponent<CameraMouseMovement>().enabled = false;
            return;
        }

		if (!lockMouse)
		{
			return;
		}

		Cursor.visible = false; //Oculta o cursor do mouse
		Cursor.lockState = CursorLockMode.Locked; //Trava o cursor do centro
	}


	void Update()
	{

		if(!lockCamera)
		{
			mouseX += Input.GetAxis("Mouse X") * sensitivity; // Incrementa o valor do eixo X e multiplica pela sensibilidade
			mouseY -= Input.GetAxis("Mouse Y") * sensitivity; // Incrementa o valor do eixo Y e multiplica pela sensibilidade. (Obs. usamos o - para inverter os valores)

			transform.eulerAngles = new Vector3(mouseY, mouseX, 0); //Executa a rotação da câmera de acordo com os eixos
			//rightHand.transform.eulerAngles = new Vector3(mouseY, mouseX, 0); //Executa a rotação da câmera de acordo com os eixos
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			lockCamera = (!(lockCamera));
		}
		
	}
}
