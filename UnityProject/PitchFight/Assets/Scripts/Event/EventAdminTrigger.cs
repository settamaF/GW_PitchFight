using UnityEngine;
using System.Collections;

public class EventAdminTrigger : EventTrigger
{

	public GameObject DollarMalus;
	private GameObject _dollarMalus;
	
	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if (Triggered || other.tag != "Player")
			return;
		base.OnTriggerEnter2D(other);
		GameUI.Get.SetEvent("Vous avez trois mois de payement en retard !!!", Pedago.ADMIN);
		ExecuteEvent();
	}
	
	protected override void Update()
	{
	}
	
	public override void ExecuteEvent()
	{
		if (DollarMalus)
		{
			_dollarMalus = Instantiate(DollarMalus) as GameObject;
			_dollarMalus.GetComponent<DollarsMalus>().Trigger = this;
		}
	}
	
	public override void EndEvent()
	{
		Destroy(_dollarMalus);
		base.EndEvent();
	}
}
