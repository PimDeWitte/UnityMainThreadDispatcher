# UnityMainThreadDispatcher

A thread-safe way of dispatching IEnumerator functions to the main thread in unity. Useful for calling UI functions and other actions that Unity limits to the main thread from different threads. 
### Version
0.1 - Note that this has not yet been tested in production and has been developed for a game that is still in progress. 
### Installation

No dependencies needed other than Unity. This script was created in Unity 5.3, and has been tested in 5.3. 

1. Download the UnityMainThreadDispatcher prefab and add it to your scene, or simple create an empty GameObject, call it UnityMainThreadDispatcher.
2. Download the UnityMainThreadDispatcher.cs script and add it to your prefab
3. You can now dispatch objects to the main thread in Unity.

###Usage
```C#
	public IEnumerator ThisWillBeExecutedOnTheMainThread() {
		Debug.Log ("This is executed from the main thread");
		yield return null;
	}
	public void ExampleMainThreadCall() {
		UnityMainThreadDispatcher.Instance().Enqueue(ThisWillBeExecutedOnTheMainThread()); 
	}
```

### Development

Want to contribute? Great! If you find a bug or want to make improvements, simply fork the repo and make a pull request with your changes on your own fork.

### Author
@PimDeWitte






 
