using UnityEngine;

public class Patch : MonoBehaviour
{
    //When player collides with patch, increase health
    private void OnTriggerEnter(Collider other)
    {
       PlayerHandler ph = other.GetComponent<PlayerHandler>();
        if(ph != null )
        {
            ph.IncreaseHealth();
            Destroy(this.gameObject);
        }
    }
}
