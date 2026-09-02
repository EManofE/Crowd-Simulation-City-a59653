using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

	public GameObject[] goals;
	NavMeshAgent agent;
	Animator anim;

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
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(agent.remainingDistance < 1)
		{
			int i = Random.Range(0,goals.Length);
			agent.SetDestination(goals[i].transform.position);
		;
		}
	}
}
