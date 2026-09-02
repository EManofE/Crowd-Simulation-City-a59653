using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

	public GameObject[] goals;
	NavMeshAgent agent;
	Animator anim;
	float speedmult;
	float detectionRadius = 10;
	float fleeRadius = 10;


	// Use this for initialization
	void Start () {

		agent = this.GetComponent<NavMeshAgent>();
		goals = GameObject.FindGameObjectsWithTag("goal");
		int i = Random.Range(0,goals.Length);

		agent.SetDestination(goals[i].transform.position);
		anim = this.GetComponent<Animator>();
		anim.SetTrigger("isWalking");
		anim.SetFloat("woff", Random.Range(0.0f, 1.0f));
		float sm = Random.Range(0.5f,2.0f);
		anim.SetFloat("spoff", sm);
		agent.speed *=sm;
		Resetagent();
	}
	void Resetagent()
	{
		speedmult = Random.Range(0.5f, 1.2f);
		anim.SetFloat("spult", speedmult);
		agent.speed *= speedmult;
		anim.SetTrigger("isWalking");
		agent.angularSpeed = 120;
		agent.ResetPath();
	}

	public void DetectNewObstacle(Vector3 position)
	{
		if(Vector3.Distance(position, this.transform.position) < detectionRadius)
		{
			Vector3 fleeDirection = (this.transform.position - position). normalized;
			Vector3 newgoal = this.transform.position + fleeDirection * fleeRadius;

			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(newgoal, path);

			if(path.status != NavMeshPathStatus.PathInvalid)
			{
				agent.SetDestination(path.corners[path.corners.Length -1]);
				anim.SetTrigger("isRunning");
				agent.speed = 10;
				agent.angularSpeed = 500;
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(agent.remainingDistance < 1)
		{
			Resetagent();
			int i = Random.Range(0,goals.Length);
			agent.SetDestination(goals[i].transform.position);
		
		}
	}
}
