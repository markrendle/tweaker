using System.Linq;
using CsQuery;

namespace Tweaker
{
    public class ScriptShift : ITweak
    {
        public void Run(CQ doc)
        {
            var scripts = doc["script:not([data-pin])"].ToArray();
            if (scripts.Length == 0) return;

            var body = doc["body"].First();

            foreach (var script in scripts)
            {
                body.Append(script);
            }
        }
    }
}