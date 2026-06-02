using UnityEngine;

namespace Member.KJH.Code.Animators
{
    [CreateAssetMenu(fileName = "Param", menuName = "SO/Anim/Param", order = 0)]
    public class AnimParamSO : ScriptableObject
    {
        public string paramName;
        public int hashValue;

        private void OnValidate()
        {
            if(!string.IsNullOrEmpty(paramName))
                hashValue = Animator.StringToHash(paramName);
        }
    }
}