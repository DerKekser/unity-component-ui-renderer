using System;
using Kekser.ComponentSystem.V2.ComponentBase;
using UnityEngine;

namespace Kekser.ComponentSystem.V2.ComponentUI.Test
{
    public class AppTester : MonoBehaviour
    {
        [SerializeField]
        private bool _test = false;

        private void OnValidate()
        {
            if (_test)
            {
                _test = false;
                IFragmentContext fragmentContext = new App().GetContext();
                Debug.Log(fragmentContext.Key);
            }
        }
    }
}