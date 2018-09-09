
using FoundationFramework;
using UnityEngine;

public class StartScene : MonoBehaviour
{
	private const float LoadDelay = 3;
	private const string HubScene = "Hub";

	private void Start () 
	{
		Timer.Register(LoadDelay, () => SceneLoader.Instance.LoadScene(HubScene));
	}
}
