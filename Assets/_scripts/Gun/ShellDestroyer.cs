using UnityEngine;

public class ShellDestroyer : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f); 
    }
}