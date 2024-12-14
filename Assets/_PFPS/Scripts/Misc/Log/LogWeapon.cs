using TMPro;
using UnityEngine;

namespace HeavenFalls
{
    public class LogWeapon : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpLog;

        public void Show(int currentAmmo, bool isReloading)
        {
            tmpLog.text = $"Current Ammo : {currentAmmo}\n" +
                          $"Is Reloading : {isReloading}";
        }
    }
}