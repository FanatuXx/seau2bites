using UnityEngine;

public class PlayerMovement : MonoBehaviour


{
    public CharacterController2D controller; //j'ai cr�� une variable charactercontroller et je l'ai nomm�e controller, elle apparaitra dans l'inspecteur et a chaque fois qu'on nommera conrtoleur �a fera ref au charachtercontroller2d qu'on lui associe dans l'inspecteur
    public Animator animator; //Variable animator visible dans l'inspecteur

    public float runSpeed = 40f; // j'ai cr�� une variable vitesse 
    float horizontalMove = 0f; // j'ai cr�� une variable horizontalmove 
    bool jump = false; // j'ai cr�� une variable bol�eene qui ne peut etre que V ou F et je l'ai mise comme F par d�faut
    bool crouch = false;

    // Update est appell�e une fois par frame, lieu ou on prend l'input du joueur
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //Valeur -1 (G) ou +1 (D), clavier, zqsd et joystick. On multiplie par la vitesse
       

        if (Input.GetButtonDown("Jump"))
        {
            jump = true; //si on appuie sur le bouton "jump" alors la variable bol�enne jump devient vraie
           
        }
        if (Input.GetButtonDown("Crouch"))

        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

  
    // FixedUpdate est appell� toutes les X secondes (fixe), fonction ou on va appliquer l'input du joueur pour bouger notre perso
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); //on bouge le perso en fonction de la variable horizontalMove, time.deltatime corrige en fonction du nombre de frame
        jump = false; //pour que la variable redevienne fausse apr�s avoir saut�, sinon saut � l'infini

    }
}

