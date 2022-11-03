using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambateJugador : MonoBehaviour
{
    public float vida;
    private Player player;

    public float tiempoPerdidaControl;
    
//se refiere a la animacion del player
//private Animator animator
    private void Start() {
        player = GetComponent<Player>();

    }

    public void TomarDaño(float daño){
        vida-=daño;
    }

    public void TomarDaño(float daño,Vector2 posicion){
        player.Rebote(posicion);
        vida-=daño;

        //animacion de golpe
        //animator.SetTrigger("Golpe");
    }

    
}
