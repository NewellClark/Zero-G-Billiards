using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codonbyte.SpaceBilliards
{
    [Serializable]
    public class UnifiedTimedStringQueue : IUnifiedContainer<ITimedQueue<string>> { }

    [Serializable]
    public class UnifiedICueRig : IUnifiedContainer<ICueRig> { }

    [Serializable]
    public class UnifiedIPocketSelectionButton : IUnifiedContainer<IPocketSelectionButton> { }

    [Serializable]
    public class UnifiedIPocketSet : IUnifiedContainer<IPocketSet> { }
}
