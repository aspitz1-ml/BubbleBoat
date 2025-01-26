    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;
    
    public class Restart : MonoBehaviour {
    
    	public void RestartGame() {
            Debug.Log("Restarting game...");
    		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    	}
    
    }