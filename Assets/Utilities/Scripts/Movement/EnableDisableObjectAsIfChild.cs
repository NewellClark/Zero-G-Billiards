using UnityEngine;
using System.Collections;

namespace Codonbyte
{
    [ExecuteInEditMode]
    public class EnableDisableObjectAsIfChild : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] adoptedChildren;

        private void UpdateEnabledState()
        {
            if (adoptedChildren == null) return;
            foreach (var child in adoptedChildren)
            {
                if (child == null) continue;
                child.SetActive(gameObject.activeSelf);
            }
        }

        void OnEnable()
        {
            UpdateEnabledState();
        }

        void OnDisable()
        {
            UpdateEnabledState();
        }
    } 
}
