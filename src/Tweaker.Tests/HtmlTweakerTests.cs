namespace Tweaker.Tests
{
    using Xunit;

    public class HtmlTweakerTests
    {
        /// <summary>
        /// In general, all script tags should be at the end of the <c>body</c> element.
        /// </summary>
        [Fact]
        public void MovesScriptTagsToEndOfBodyElement()
        {
            const string source = @"<html><head><script src=""jquery.js""></script></head><body><h1>Hello</h1></body></html>";
            const string expected = @"<html><head></head><body><h1>Hello</h1><script src=""jquery.js""></script></body></html>";

            var actual = HtmlTweaker.Default(source);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Need a way to "pin" scripts like html5shiv so that they stay where you put them (e.g. in the <c>head</c> element)
        /// </summary>
        [Fact]
        public void DoesNotMovePinnedScriptTags()
        {
            const string source = @"<html><head><script data-pin src=""html5shiv.js""></script><script src=""jquery.js""></script></head><body><h1>Hello</h1></body></html>";
            const string expected = @"<html><head><script data-pin src=""html5shiv.js""></script></head><body><h1>Hello</h1><script src=""jquery.js""></script></body></html>";

            var actual = HtmlTweaker.Default(source);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PutsAllCssLinksTogether()
        {
            const string source = @"<html><head><link rel=""stylesheet"" href=""a.css""><script data-pin src=""html5shiv.js""></script><link rel=""stylesheet"" href=""b.css""></head><body><h1>Hello</h1></body></html>";
            const string expected = @"<html><head><link rel=""stylesheet"" href=""a.css""><link rel=""stylesheet"" href=""b.css""><script data-pin src=""html5shiv.js""></script></head><body><h1>Hello</h1></body></html>";
            var actual = HtmlTweaker.Default(source);
            Assert.Equal(expected, actual);
        }
    }
}
