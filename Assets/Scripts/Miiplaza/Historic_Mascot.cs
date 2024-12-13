using UnityEngine;

public class Historic_Mascot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
