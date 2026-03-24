using UnityEngine;
using UnityEngine.InputSystem;

public class ARShoot : MonoBehaviour
{
    public InputActionReference tapAction; //"Touchscreen Gestures/Tap Start Position" 
    public GameObject projectile; 
    public float shotSpeed = 4f; 
      
    
    void OnEnable() { 
        tapAction.action.performed += OnTapDetected; 
        //tapAction.action.Enable(); 
    }
    
    void OnDisable() { 
        tapAction.action.performed -= OnTapDetected; 
        //tapAction.action.Disable(); 
    } 
    
    public void OnTapDetected(InputAction.CallbackContext context) 
    { 
        Debug.Log("SHOOT"); // see in ingameDebugConsole
        
        GameObject newProjectile = Instantiate(projectile); 
        
        newProjectile.transform.position = transform.position; 
        Vector3 shotVelocity = transform.forward * shotSpeed; 
        newProjectile.GetComponent<Rigidbody>().linearVelocity = shotVelocity; 
    
    } 
}
