using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

	public float moveSpeed = 25.0f;
	public float moveTurnSpeed = 3.0f;
	float m_RunCycleLegOffset = 0.2f;
	float m_StationaryTurnSpeed = 18;
	//public float underwaterSpeed = 10.0f;
	//public bool isUnderwaterD = false;
	//public bool isDrowningD = false;

	GameObject MainCamera;
	private Vector3 VectorCamera;

	//private float PosY;
	private Vector3 velocity;
	public Rigidbody rigidbody;
	public CapsuleCollider collider;

	//private bool IsGrounded = true ;

	IComparer rayHitComparer;

	/*private float jumpPower = 30;
	public float jumpCooldown = 0;
	public bool jump;
	private float distance = 1;*/

	//public bool IsOut;

	/*public Scrollbar Water;
	public GameObject WaterBar;
	public float Air = 1f;*/

	Animator animator;

	float m_TurnAmount;
	float m_ForwardAmount;
	float translation;
	float rotation;
	float MovingRotation;

	float angleY;

	void Start()
	{
		animator = GetComponent<Animator>();
		MainCamera = GameObject.Find ("Main Camera Main");
	}

	void FixedUpdate() 
	{

		//si la camera a un angle different et qu'on appuie sur vertical 

		float actualXCam = MainCamera.transform.rotation.eulerAngles.x;
		float actualYCam = MainCamera.transform.rotation.eulerAngles.y;
		float actualZCam = MainCamera.transform.rotation.eulerAngles.z;

		float actualXPla = transform.rotation.eulerAngles.x;
		float actualYPla = transform.rotation.eulerAngles.y;
		float actualZPla = transform.rotation.eulerAngles.z;

		MainCamera.transform.rotation = Quaternion.Euler (actualXCam,actualYCam,actualZCam);
		transform.rotation = Quaternion.Euler (actualXPla,actualYPla,actualZPla);

		if(Input.GetAxis ("Vertical") > 0 && MainCamera.transform.rotation.eulerAngles.y != transform.rotation.eulerAngles.y)
		{
			transform.rotation = Quaternion.Euler (actualXPla,actualYCam,actualZPla);
		} 


		//animation
		float v = Input.GetAxis("Vertical");


		//deplacer le joueur
		//MovingRotation = moveTurnSpeed * Input.GetAxis ("Vertical");

		translation = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;

		if (Input.GetAxis ("Vertical") == 0) { // not moving
			rotation = Input.GetAxis ("Horizontal") * moveTurnSpeed / 10;
			m_TurnAmount = Mathf.Atan2 (rotation, rotation);


		} else if (Input.GetAxis ("Vertical") < 0) { // going back
			m_TurnAmount = Mathf.Atan2 (rotation, rotation);
			rotation = -Input.GetAxis ("Horizontal") * moveTurnSpeed / 15;

		} else { // straight
			rotation = Input.GetAxis ("Horizontal") * moveTurnSpeed;
			m_TurnAmount = Mathf.Atan2 (0, 0);
		}



		transform.Translate(0, 0, translation);
		transform.Rotate(0, rotation, 0);




		m_ForwardAmount = translation;

		animator.SetFloat("Forward", m_ForwardAmount*50, 0.1f, Time.deltaTime);
		animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		/*
		float runCycle =
			Mathf.Repeat(
				animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
*/
		animator.SetFloat("Forward",v);
		velocity = rigidbody.velocity;
	}
}





/*if (Input.GetAxis ("Vertical") > 0) {
			/*translation = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;
			m_TurnAmount = Mathf.Atan2 (0, 0);
			MovingRotation = moveTurnSpeed * Input.GetAxis ("Vertical");*/
/*m_TurnAmount = Mathf.Atan2 (0, 0);
translation = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;
MovingRotation = moveTurnSpeed * Input.GetAxis ("Vertical");
} else if (Input.GetAxis ("Vertical") < 0){
	m_TurnAmount = Mathf.Atan2 (0, 0);
	translation = (Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime)/2;
	MovingRotation = moveTurnSpeed * Input.GetAxis ("Vertical");
} else if (Input.GetAxis ("Vertical") == 0){
	MovingRotation = moveTurnSpeed;
	m_TurnAmount = Mathf.Atan2(rotation,rotation);
}*/
//angleY = (axeDeDirection.y / (Mathf.Sqrt(1-axeDeDirection.w * axeDeDirection.w)));
//print (angleY);

//angleY = 2 * Mathf.Acos (axeDeDirection.y);

//print("sinY " + Mathf.Asin (axeDeDirectionJoueur.y) * 100);
//print("cosY " + Mathf.Acos (axeDeDirectionJoueur.y) * 100);
//print ( transform.rotation.eulerAngles.y);


//print(MainCamera.transform.rotation);
//print(transform.rotation);