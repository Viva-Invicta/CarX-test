using UnityEngine;

namespace TowerDefence.Components
{
    public class Damager : MonoBehaviour
    {
        [field: SerializeField]
        public int Damage 
        {
            get; 
            private set;
        }
    }
}