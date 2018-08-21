using UnityEngine;
using FoundationFramework.Pools;

public class RocketLauncher : MonoBehaviour
{
	public GameObject RocketPrefab;


	private void Start()
	{
		//Optionally you can start by preloading objects to avoid hiccups later
		PoolManager.PopulatePool(RocketPrefab, 32);
	}

	private void Update()
	{
		if(Input.GetMouseButton(0))
		{
			Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
			SpawnBullet(Camera.main.ScreenToWorldPoint(pos), Quaternion.identity);	
		}
	}

	private void SpawnBullet(Vector3 position, Quaternion rotation)
	{
		//Rocket rocket = PoolManager.Spawn(RocketPrefab, position, rotation).GetComponent<Rocket>();
		PoolManager.Spawn(RocketPrefab, position, rotation);
	}
}
