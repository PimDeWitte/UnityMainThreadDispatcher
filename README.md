# UnityMainThreadDispatcher

A thread-safe way of dispatching IEnumerator functions to the main thread in unity. Useful for calling UI functions and other actions that Unity limits to the main thread from different threads. Initially written for Firebase Unity but now used across the board!

### Version
1.0 - Tested and functional in one or more production mobile games (including https://get-wrecked.com)

### Installation

No dependencies needed other than Unity. This script was created in Unity 5.3, and has been tested in 5.3, 5.4, and 5.5.

1. Download the UnityMainThreadDispatcher prefab and add it to your scene, or simple create an empty GameObject, call it UnityMainThreadDispatcher.
2. Download the UnityMainThreadDispatcher.cs script and add it to your prefab
3. You can now dispatch objects to the main thread in Unity.

### Usage
```C#
	public IEnumerator ThisWillBeExecutedOnTheMainThread() {
		Debug.Log ("This is executed from the main thread");
		yield return null;
	}
	public void ExampleMainThreadCall() {
		UnityMainThreadDispatcher.Instance().Enqueue(ThisWillBeExecutedOnTheMainThread()); 
	}
```
OR

```C#
	UnityMainThreadDispatcher.Instance().Enqueue(() => Debug.Log ("This is executed from the main thread"));
```
### Development

Want to contribute? Great! If you find a bug or want to make improvements, simply fork the repo and make a pull request with your changes on your own fork.

### Author
@PimDeWitte






 
