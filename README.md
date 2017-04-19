# UnityMainThreadDispatcher

A thread-safe way of dispatching IEnumerator functions to the main thread in unity. Useful for calling UI functions and other actions that Unity limits to the main thread from different threads. 
### Version
1.0 - Tested and functional in one or more production mobile games (including https://get-wrecked.com)

### Installation

No dependencies needed other than Unity. This script was created in Unity 5.3, and has been tested in 5.3 through 5.6.

Just download the UnityMainThreadDispatcher.cs script, and you're done. 
The script adds a singleton runner to the scene on it's own when you enter play mode, so you don't need to do anything to start using the static methods from the script.


###Usage
```C#
	public IEnumerator ThisWillBeExecutedOnTheMainThread() {
		Debug.Log ("This is executed from the main thread");
		yield return null;
	}
	public void ExampleMainThreadCall() {
		UnityMainThreadDispatcher.Enqueue(ThisWillBeExecutedOnTheMainThread()); 
	}
```
OR

```C#
	UnityMainThreadDispatcher.Enqueue(() => Debug.Log ("This is executed from the main thread"));
```
### Development

Want to contribute? Great! If you find a bug or want to make improvements, simply fork the repo and make a pull request with your changes on your own fork.

### Author
@PimDeWitte






 
