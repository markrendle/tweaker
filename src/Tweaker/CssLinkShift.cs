using System.Linq;
using CsQuery;

namespace Tweaker
{
    public class CssLinkShift : ITweak
    {
        public void Run(CQ doc)
        {
            var cssLinks = doc["link[rel='stylesheet']"].ToArray();
            if (cssLinks.Length < 2) return;

            var parent = cssLinks[0].ParentNode;

            for (int i = 1; i < cssLinks.Length; i++)
            {
                parent.InsertAfter(cssLinks[i],cssLinks[i-1]);
            }
        }
    }
}