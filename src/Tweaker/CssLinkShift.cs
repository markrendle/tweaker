using System.Linq;
using CsQuery;

namespace Tweaker
{
    using System.Diagnostics;

    public class CssLinkShift : ITweak
    {
        public void Run(CQ doc)
        {
            try
            {
                var first = doc["head"]["link[rel='stylesheet'],style"].First();
                doc["link[rel='stylesheet'],style"].Slice(1).InsertAfter(first);
            }
            catch (System.Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}