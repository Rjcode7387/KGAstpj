using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviourPun
{
	public Renderer render;

	private float healAmount;//힐량 랜덤

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player")
		{
			other.SendMessage("Heal", healAmount);
		}
		Destroy(gameObject);
    }

    private void Awake()
    {
        //photonnetwork.instantiate 호출시 함깨 보낸data 파리미터
        object[] param = photonView.InstantiationData;

        if (param != null)
        {
            Vector3 colorVector = (Vector3)param[0];
            float healAmount = (float)param[1];

            render.material.color = new Color(colorVector.x, colorVector.y, colorVector.z);
            this.healAmount = healAmount;
        }
    }
    private void Reset() {
		render = GetComponentInChildren<Renderer>();

		
	}
}

