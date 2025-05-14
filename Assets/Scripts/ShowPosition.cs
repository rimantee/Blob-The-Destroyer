using UnityEngine;

public class ShowPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public float axisLength = 1f; void FixedUpdate()
    {
        Vector3 pos = transform.position;        // Draw local (object) axes        
        Debug.DrawLine(pos, pos + transform.right * axisLength, Color.red);    // Local X axis       
        Debug.DrawLine(pos, pos + transform.up * axisLength, Color.green);     // Local Y axis        
        Debug.DrawLine(pos, pos + transform.forward * axisLength, Color.blue); // Local Z axis        

        // Draw world axes for reference(offset slightly to not overlap)
        Vector3 offset = new Vector3(0.2f, 0.2f, 0.2f); // Prevent overlap      
        Debug.DrawLine(pos + offset, pos + offset + Vector3.right * axisLength, new Color(1, 0.5f, 0.5f)); // World X        
        Debug.DrawLine(pos + offset, pos + offset + Vector3.up * axisLength, new Color(0.5f, 1, 0.5f));    // World Y        
        Debug.DrawLine(pos + offset, pos + offset + Vector3.forward * axisLength, new Color(0.5f, 0.5f, 1)); // World Z    
    }
}
