using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class Controller : MonoBehaviour
{

    public float speed = 6.0f;
    private GameObject cameraFps;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float rotacaoX = 0;
    private float rotacaoY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraFps = GetComponentInChildren(typeof(Camera)).transform.gameObject;
        cameraFps.transform.localPosition = new Vector3(0, 1, 0);
        cameraFps.transform.localRotation = Quaternion.identity;
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //apenas movimenta se estiver no chao
        if (controller.isGrounded)
        {
            //pega a posicao a face à frente da camera
            Vector3 direcaoFrente = new Vector3(cameraFps.transform.forward.x, 0, cameraFps.transform.forward.z);
        //pefa a direcao da face ao lado da camera
        Vector3 direcaoLado = new Vector3(cameraFps.transform.right.x, 0, cameraFps.transform.right.z);
        //normaliza os valores para o maximo de 1, para que o jogador ande sempre na mesma velocidade
        direcaoFrente.Normalize();
        direcaoLado.Normalize();
        //pega o valor das teclas para cima(1) e para baixo(-1)
        direcaoFrente = direcaoFrente * Input.GetAxis("Vertical");
        //pega o valor das teclas para direita(1) e para esquerda(-1) 
        direcaoLado = direcaoLado * Input.GetAxis("Horizontal");

        //soma a movimentação lateral com a movimentação para frente/tras
        Vector3 direcaoFinal = direcaoFrente + direcaoLado;
        if (direcaoFinal.sqrMagnitude > 1)
        {
            direcaoFinal.Normalize();
        }
        
            moveDirection = new Vector3(direcaoFinal.x, 0, direcaoFinal.z);
            moveDirection = moveDirection * speed;

            if (Input.GetButton(("Jump")))
            {
                moveDirection.y = 8.0f;
            }


            //multiplica a velocidade que foi consigurada no jogador
            
        }
        //faz o jogador ir para baixo(gravidade)
        moveDirection.y -= 20.0f * Time.deltaTime;

        // faz o movimento
        controller.Move(moveDirection * Time.deltaTime);
    }
}
