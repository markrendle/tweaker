namespace Tweaker
{
    using System;
    using System.Linq;
    using CsQuery;

    public class HtmlTweaker
    {
        private readonly Action<CQ> _action;
        private static readonly HtmlTweaker DefaultInstance = new HtmlTweaker(ScriptShift, CssLinkShift);

        public HtmlTweaker(params Action<CQ>[] actions)
        {
            _action = (Action<CQ>)Delegate.Combine(actions.Cast<Delegate>().ToArray());
        }

        public string Transform(string html)
        {
            var doc = CQ.Create(html);
            _action(doc);
            return doc.Render();
        }

        public static string Default(string html)
        {
            return DefaultInstance.Transform(html);
        }

        public static void ScriptShift(CQ doc)
        {
            var scripts = doc["script:not([data-pin])"].ToArray();
            if (scripts.Length == 0) return;

            var body = doc["body"].First();

            foreach (var script in scripts)
            {
                body.Append(script);
            }
        }

        public static void CssLinkShift(CQ doc)
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