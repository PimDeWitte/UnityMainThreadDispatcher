using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// A thread-safe class which holds a queue with actions to execute on the next Update() method. It can be used to make calls to the main thread for 
/// things such as UI Manipulation in Unity. It was developed for use in combination with the Firebase Unity plugin, which uses separate threads for event handling
/// </summary>
public class UnityMainThreadDispatcher : MonoBehaviour {

	private readonly static Queue<Action> _executionQueue = new Queue<Action>();

	public void Update() {
		while (_executionQueue.Count > 0)
		{
			_executionQueue.Dequeue().Invoke();
		}
	}

	/// <summary>
	/// Locks the queue and adds the action to the queue
	/// </summary>
	/// <param name="action">IEnumerator function that will be executed from the main thread.</param>
	public void AddAction(IEnumerator action) {
		lock (_executionQueue) {
			_executionQueue.Enqueue (() => {  
				StartCoroutine (action); 
			});
		}
	}


	private static UnityMainThreadDispatcher _instance = null;
	private bool _isInitialized = false;

	public static bool Exists() {
		return _instance != null;
	}
		
	public static UnityMainThreadDispatcher Instance() {
		if (!Exists ()) {
			throw new Exception ("UnityMainThreadDispatcher could not find the UnityMainThreadDispatcher object. Please ensure you have added the MainThreadExecutor Prefab to your scene.");
		} 
		return _instance;
	}
		

	void Awake() {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	void OnDestroy() {
			_instance = null;
	}

		
}


