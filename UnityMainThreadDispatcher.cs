/*
Copyright 2015 Pim de Witte All Rights Reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.Callbacks;

/// Author: Pim de Witte (pimdewitte.com) and contributors
/// <summary>
/// A thread-safe class which holds a queue with actions to execute on the next Update() method. It can be used to make calls to the main thread for
/// things such as UI Manipulation in Unity. It was developed for use in combination with the Firebase Unity plugin, which uses separate threads for event handling
/// </summary>
public class UnityMainThreadDispatcher : MonoBehaviour {

	private static readonly Queue<IEnumerator> _executionQueue = new Queue<IEnumerator>();
	private static UnityMainThreadDispatcher _instance;

	/// <summary>
	/// Locks the queue and adds the IEnumerator to the queue
	/// </summary>
	/// <param name="action">IEnumerator function that will be executed from the main thread.</param>
	public static void Enqueue(IEnumerator action) {
		lock (_executionQueue) {
			_executionQueue.Enqueue(action);
		}
	}

	/// <summary>
	/// Locks the queue and adds the Action to the queue
	/// </summary>
	/// <param name="action">function that will be executed from the main thread.</param>
	public static void Enqueue(Action action) {
		Enqueue(_instance.ActionWrapper(action));
	}


	/// <summary>
	/// This ensures that there's exactly one UnityMainThreadDispatcher in every scene, so the singleton will exist no matter which scene you play from.
	/// </summary>
	[PostProcessScene]
	private static void AddDispatcherToScene() {
		var dispatcherContainer = new GameObject("UnityMainThreadDispatcher");
		DontDestroyOnLoad(dispatcherContainer);
		dispatcherContainer.AddComponent<UnityMainThreadDispatcher>();
	}

	private void Awake() {
		if (_instance != null) {
			Destroy(gameObject);
		}
		else {
			_instance = this;
		}
	}

	private void Update() {
		lock(_executionQueue) {
			while (_executionQueue.Count > 0) {
				StartCoroutine(_executionQueue.Dequeue());
			}
		}
	}

	IEnumerator ActionWrapper(Action a) {
		a();
		yield return null;
	}
}

