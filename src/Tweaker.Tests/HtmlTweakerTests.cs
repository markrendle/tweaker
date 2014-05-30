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

        [Fact]
        public void MaintainsLinkAndScriptOrder()
        {
            const string source = @"<html><head><link rel=""stylesheet"" href=""a.css""><style>body {padding-top:60px}</style><script data-pin src=""html5shiv.js""></script><link rel=""stylesheet"" href=""b.css""></head><body><h1>Hello</h1></body></html>";
            const string expected = @"<html><head><link rel=""stylesheet"" href=""a.css""><style>body {padding-top:60px}</style><link rel=""stylesheet"" href=""b.css""><script data-pin src=""html5shiv.js""></script></head><body><h1>Hello</h1></body></html>";
            var actual = HtmlTweaker.Default(source);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MovesStyleTagsFromBodyToHead()
        {
            const string source = @"<html><head><link rel=""stylesheet"" href=""a.css""><style>body {padding-top:60px}</style><script data-pin src=""html5shiv.js""></script><link rel=""stylesheet"" href=""b.css""></head><body><style>.foo {color: red}</style><h1>Hello</h1><style>.bar {color: blue}</style></body></html>";
            const string expected = @"<html><head><link rel=""stylesheet"" href=""a.css""><style>body {padding-top:60px}</style><link rel=""stylesheet"" href=""b.css""><style>.foo {color: red}</style><style>.bar {color: blue}</style><script data-pin src=""html5shiv.js""></script></head><body><h1>Hello</h1></body></html>";
            var actual = HtmlTweaker.Default(source);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChangesImgSource()
        {
            const string source = @"<html><head></head><body><img src=""/images/default-source/foo.png""></body></html>";
            const string expected = @"<html><head></head><body><img src=""http://my.cdn.com/media/foo.png""></body></html>";
            var actual = HtmlTweaker.Default(source);
            Assert.Equal(expected, actual);
        }
    }
}
