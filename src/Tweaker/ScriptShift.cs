using System.Linq;
using CsQuery;

namespace Tweaker
{
    using System.Diagnostics;

    public class ScriptShift : ITweak
    {
        public void Run(CQ doc)
        {
            try
            {
                doc["script:not([data-pin])"].AppendTo(doc["body"]);
            }
            catch (System.Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}