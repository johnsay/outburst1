using System;
using System.Collections.Generic;

namespace FoundationFramework
{
    [Serializable]
    public class ContainerData
    {
        public List<ItemEntry> Items = new List<ItemEntry>();
        public bool IsInitialized = false;
    }

}

