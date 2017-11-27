using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class canavarScript : MonoBehaviour {

	// Use this for initialization
	private Transform goal;
	private NavMeshAgent agent;
	static Animator animator;
	private float mesafe;
	private bool dead;

	// Use this for initialization
	void Start()
	{

		//create references
		goal = Camera.main.transform;
		animator = gameObject.GetComponent<Animator> ();
		agent = gameObject.GetComponent<NavMeshAgent>();
		//set the navmesh agent's desination equal to the main camera's position (our first person character)
		agent.destination = goal.position;
		//start the walking animation
		dead =false;

	}

	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, goal.position) > 3 && dead==false) {
			animator.SetBool ("isWalking", true);
			animator.SetBool ("isAttacking", false);
		} else if (Vector3.Distance (transform.position, goal.position) <= 3 && dead==false) {
			animator.SetBool ("isWalking", false);
			animator.SetBool ("isAttacking", true);
		
		} else if(dead) {
			animator.SetBool ("isWalking", false);
			animator.SetBool ("isAttacking", false);
			animator.SetBool ("isDeading", true);
		}


	}
	void OnTriggerEnter(Collider col)
	{
		animator.SetBool ("isWalking", false);
		animator.SetBool ("isAttacking", false);
		animator.SetBool ("isDeading", true);
		dead = true;

		gameObject.GetComponent<CapsuleCollider> ().enabled = false;

		Destroy (col.gameObject);

		Destroy (gameObject, 6);

		GameObject skeleton = Instantiate(Resources.Load("skeleton", typeof(GameObject))) as GameObject;


		float randomX = UnityEngine.Random.Range(-12f, 12f);
		float constantY = .01f;
		float randomZ = UnityEngine.Random.Range(-13f, 13f);
	
		skeleton.transform.position = new Vector3(randomX, constantY, randomZ);
		dead = false;





		while (Vector3.Distance(skeleton.transform.position, Camera.main.transform.position) <= 3)
		{

			randomX = UnityEngine.Random.Range(-12f, 12f);
			randomZ = UnityEngine.Random.Range(-13f, 13f);

			skeleton.transform.position = new Vector3(randomX, constantY, randomZ);
		}

	}
}
