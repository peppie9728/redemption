using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private async void OnParticleCollision(GameObject other)
    {
       if(other.layer == 6)
       {
            await Task.Delay(1000);
            other.GetComponent<Enemy>().health -= 2;
       }
    }
}
