using System;
using Kekser.ComponentUI;
using UnityEngine;

namespace Example.Providers
{
    public class CountingProvider: UIProvider
    {
        public void Increment()
        {
            Props.Set("count", Props.Get("count", 0) + 1);
        }
        
        public int GetCount()
        {
            return Props.Get("count", 0);
        }
    }
}