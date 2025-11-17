using UnityEngine;

public class DDOL : MonoBehaviour
{
    private void Awake()
    {
        if(gameObject.tag == "event")
        {
            GameObject[] events = GameObject.FindGameObjectsWithTag(gameObject.tag);
            foreach (GameObject e in events) {
                if (e.name == gameObject.name && e != gameObject)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
