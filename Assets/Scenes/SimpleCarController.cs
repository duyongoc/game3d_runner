using UnityEngine;
using System.Collections;

//thanks to James Arndt for the help with this script https://www.youtube.com/channel/UC8wwKKr-GpntrUnFpN15kVw
public class SimpleCarController : MonoBehaviour {

	private Vector3 accel;
	private float throttle;
	private float deadZone = 0.001f;
	private Vector3 myRight;
	private Vector3 velo;
	private Vector3 flatVelo;
	private Vector3 relativeVelocity;
	private Vector3 dir;
	private Vector3 flatDir;
	private Vector3 carUp;
	private Transform carTransform;
	private Rigidbody carRigidbody;
	private Vector3 engineForce;
	private bool exploded;
	private Vector3 turnVec;
	private  Vector3 imp;
	private float rev;
	private float actualTurn;
	private float carMass;
	private Transform[] wheelTransform = new Transform[4];

	private float actualGrip;
	private float horizontal;
	public  float maxSpeedToTurn = 18f;
	public  GameObject explodePrefab;

	public Transform rearLeftSkidGenerator;
	public Transform rearRightSkidGenerator;
	public Transform carBody;

	public float power = 300;
	public float maxSpeed = 50;
	public float minimumSlideSpeed = 13;
	public float carGrip = 70;
	public enum  turnSpeedEnum{nromal,sharp};
	public turnSpeedEnum selectTurnType;
	public float turnSpeed; //3f;
	
	private float slideSpeed;
	private float mySpeed;
	public Transform centerOfMass;

	private Vector3 carRight;
	private Vector3 carFwd;
	private Vector3 tempVEC;

	public Transform skidTrailPrefab;

	// public static Transform skidTrailsDetachedParentLeft;
	// private Transform skidTrailLeft;
	// public static Transform skidTrailsDetachedParentRight;
	// private Transform skidTrailRight;
	private bool buttonDown = false;
	private bool leavingSkidTrail;
	public bool justDemo; //true if we just want the car to keep spinning for main menu or demos
	// Use this for initialization
	void Start () {

		Initialize();
	}

	 void Initialize() {
	
		switch(selectTurnType) {
		
		case turnSpeedEnum.nromal:
			turnSpeed = 3.4f;
			break;

		case turnSpeedEnum.sharp:
			turnSpeed = 6;
			break;
		}

		carTransform = transform;

		carRigidbody = GetComponent<Rigidbody>();

		carUp = carTransform.up;

		carMass = carRigidbody.mass;

		carFwd = Vector3.forward;

		carRight = Vector3.right;

		carRigidbody.centerOfMass = centerOfMass.localPosition;

		// if (skidTrailsDetachedParentLeft == null)
		// {
		// 	skidTrailsDetachedParentLeft = new GameObject("Skid Trails Left").transform;
		// }

		// if (skidTrailsDetachedParentRight == null)
		// {
		// 	skidTrailsDetachedParentRight = new GameObject("Skid Trails Right").transform;
		// }
	}

	void FixedUpdate() {

		roateBody();
		carPhysicsUpdate();
		//doSkidTrail();

		if(Input.GetMouseButton(0))
        {
            float moveTurn = Input.mousePosition.x;

            if(moveTurn < Screen.width / 2 && moveTurn > 0)
            {
                turnLeft();
            }
            else if(moveTurn > Screen.width / 2 && moveTurn < Screen.width)
            {
				turnRight();

            }
            
        }
		else 
            {
				engineForce = Vector3.forward;
            }
		// //throttle = Input.GetAxis("Vertical");



		if(mySpeed < maxSpeed) {
		
			carRigidbody.AddForce(engineForce * 5 * Time.deltaTime);

		}

		if(mySpeed > maxSpeedToTurn && ( minimumSlideSpeed > slideSpeed && slideSpeed > -minimumSlideSpeed)) {
		
			carRigidbody.AddTorque(turnVec * Time.deltaTime);

		}
		// else if (mySpeed < maxSpeedToTurn) {
		// 	return;

		// }
		
		//transform.Translate(Vector3.forward * 5.0f  * Time.deltaTime, Space.World);
      
        
	}
						
	// Update is called once per frame
	void Update () {


	}

	void carPhysicsUpdate() {
	
	
		myRight = carTransform.right;

		velo = carRigidbody.velocity;


		tempVEC = new Vector3(velo.x,0.0f,velo.z);

		flatVelo = tempVEC;

		dir = transform.TransformDirection(carFwd);

		tempVEC = new Vector3(dir.x,0.0f,dir.z);

		flatDir = Vector3.Normalize(tempVEC);

		relativeVelocity = carTransform.InverseTransformDirection(flatVelo);

		slideSpeed = Vector3.Dot(myRight,flatVelo);

		mySpeed = flatVelo.magnitude;  

		rev = Mathf.Sign(Vector3.Dot(flatVelo,flatDir));

		engineForce = (flatDir * (power * throttle) * carMass);


		actualTurn = horizontal;

		if(rev < 0.1f) {
		
			//actualTurn = -actualTurn;
		}


		turnVec = ((( carUp * turnSpeed) * actualTurn ) * carMass) * 800;


		actualGrip = Mathf.Lerp(100,carGrip,mySpeed * 0.02f);
		imp = myRight * (-slideSpeed * carMass * actualGrip);


	}

	void slowVelocity() {
	
		carRigidbody.AddForce(-flatVelo * 0.8f);
	}

	void roateBody() {
	
		Quaternion target;

		if(horizontal > 0) { //later change this to what button is pressed intead of horizontal 
		

			 target = Quaternion.Euler (0, 0, 3.78f);

			carBody.localRotation = Quaternion.Lerp(carBody.transform.localRotation,target,Time.deltaTime);
		}
		else if (horizontal < 0) {

			target = Quaternion.Euler (0, 0, -3.78f);
			
			carBody.localRotation = Quaternion.Lerp(carBody.transform.localRotation,target,Time.deltaTime);
		}
		else if(horizontal == 0) {
		
			target = Quaternion.Euler (0, 0, 0);
			
			carBody.localRotation = Quaternion.Lerp(carBody.transform.localRotation,target,Time.deltaTime);
		}
	}

	// void doSkidTrail() {
	
	
	// 	if (skidTrailPrefab != null)
	// 	{
	// 		if (horizontal != 0) //we are turning
	// 		{
	// 			if (!leavingSkidTrail)
	// 			{
	// 				skidTrailLeft = Instantiate(skidTrailPrefab) as Transform;
	// 				if (skidTrailLeft != null)
	// 				{
	// 					skidTrailLeft.parent = transform;
	// 					skidTrailLeft.localPosition = rearLeftSkidGenerator.transform.localPosition;
	// 				}

	// 				skidTrailRight = Instantiate(skidTrailPrefab) as Transform;
	// 				if (skidTrailRight != null)
	// 				{
	// 					skidTrailRight.parent = transform;
	// 					skidTrailRight.localPosition = rearRightSkidGenerator.transform.localPosition;
	// 				}

	// 				leavingSkidTrail = true;
	// 			}
				
	// 		}
	// 		else
	// 		{
	// 			if (leavingSkidTrail)
	// 			{
	// 				skidTrailLeft.parent = skidTrailsDetachedParentLeft;
	// 				Destroy(skidTrailLeft.gameObject, 10);

	// 				skidTrailRight.parent = skidTrailsDetachedParentRight;
	// 				Destroy(skidTrailRight.gameObject, 10);

	// 				leavingSkidTrail = false;
	// 			}


	// 		}
	// 	}

	// }

	void OnCollisionEnter(Collision collision) {

		
	}

	public void turnRight() {
	
		horizontal = 1;
		buttonDown = true;

	}

	public void turnLeft() {
		horizontal = -1;
		buttonDown = true;
	}

	public void stopTuning() {
	
		horizontal = 0;
		buttonDown = false;
	}





}
