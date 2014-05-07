namespace Tweaker.Tests
{
    using System.IO;
    using System.Text;
    using Xunit;

    public class FilterStreamTests
    {
        [Fact]
        public void WritesToStreamWhenAtEndOfHtml()
        {
            using (var wrapped = new MemoryStream())
            {
                using (var target = new FilterStream(wrapped, s => s))
                {
                    const string html = "<html></html>";
                    var bytes = Encoding.UTF8.GetBytes(html);
                    target.Write(bytes, 0, bytes.Length);
                    target.Flush();
                    wrapped.Position = 0;
                    Assert.Equal(html, Encoding.UTF8.GetString(wrapped.ToArray()));
                }
            }
        }
        
        [Fact]
        public void WritesToStreamWhenEndHtmlTagIsSplit()
        {
            using (var wrapped = new MemoryStream())
            {
                using (var target = new FilterStream(wrapped, s => s))
                {
                    const string html = "<html></html>";
                    var bytes = Encoding.UTF8.GetBytes(html.Substring(0, 10));
                    target.Write(bytes, 0, bytes.Length);
                    bytes = Encoding.UTF8.GetBytes(html.Substring(10));
                    target.Write(bytes, 0, bytes.Length);
                    target.Flush();
                    wrapped.Position = 0;
                    Assert.Equal(html, Encoding.UTF8.GetString(wrapped.ToArray()));
                }
            }
        }
    }
}