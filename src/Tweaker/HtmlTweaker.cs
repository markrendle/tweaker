using System.Collections.Generic;

namespace Tweaker
{
    using System;
    using System.Linq;
    using CsQuery;

    public class HtmlTweaker
    {
        private readonly Action<CQ> _action;
        private static readonly HtmlTweaker DefaultInstance = ConstructDefault();

        public HtmlTweaker(IEnumerable<Action<CQ>> actions)
        {
            _action = (Action<CQ>)Delegate.Combine(actions.Cast<Delegate>().ToArray());
        }

        public string Transform(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return html;
            var doc = CQ.Create(html);
            if (doc["body"].Length == 0) return html;
            _action(doc);
            return doc.Render();
        }

        public static string Default(string html)
        {
            return DefaultInstance.Transform(html);
        }

        private static HtmlTweaker ConstructDefault()
        {
            var actions = new List<Action<CQ>> { new ScriptShift().Run, new CssLinkShift().Run };
            var imagesFromCdn = ImagesFromCdn.TryCreateFromAppSettings();
            if (imagesFromCdn != null)
            {
                actions.Add(imagesFromCdn.Run);
            }
            return new HtmlTweaker(actions);
        }
    }
}