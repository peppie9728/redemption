using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public float fireDamage;
    private  void OnParticleCollision(GameObject other)
    {
       if(other.layer == 6)
       {
            // await Task.Delay(2000);
            if (other != null)
                other.GetComponent<Enemy>().health -= fireDamage;
       }
    }
}
