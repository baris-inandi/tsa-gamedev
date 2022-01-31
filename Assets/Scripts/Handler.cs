using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour
{
	private static Handler _singleton;
	public static Handler Instance
	{
		get => _singleton;
		private set
		{
			if (_singleton == null)
				_singleton = value;
			else if (_singleton != value)
			{
				Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying object!");
				Destroy(value);
			}
		}
	}
	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Top Down Movement");
	}
}
