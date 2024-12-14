using TMPro;
using UnityEngine;

namespace HeavenFalls
{
    public class LogPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpLog;
        
        public void Show(Vector3 input, float velocity, bool isGrounded)
        {
            tmpLog.text = $"Input X : {input.x}\n" +
                          $"Input Z : {input.z}\n" +
                          $"Velocity : {velocity}\n" +
                          $"Is Grounded : {isGrounded}\n";
        }
    }
}