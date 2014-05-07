namespace Tweaker
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class FilterStream : Stream
    {
        private static readonly Regex EofRegex = new Regex("</html>", RegexOptions.IgnoreCase);
        private readonly StringBuilder _html;
        private readonly Stream _responseStream;
        private readonly Func<string, string> _filter;

        public FilterStream(Stream responseStream, Func<string, string> filter)
        {
            if (responseStream == null) throw new ArgumentNullException("responseStream");
            if (filter == null) throw new ArgumentNullException("filter");
            _responseStream = responseStream;
            _filter = filter;
            _html = new StringBuilder();
        }

        public override bool CanRead
        {
            get { return _responseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _responseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _responseStream.CanWrite; }
        }

        public override long Length
        {
            get { return _responseStream.Length; }
        }

        public override long Position { get; set; }

        public override void Flush()
        {
            var output = Encoding.UTF8.GetBytes(_filter(_html.ToString()));
            _responseStream.Write(output, 0, output.Length);
            _responseStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _responseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _responseStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _responseStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var html = Encoding.UTF8.GetString(buffer, offset, count);
            _html.Append(html);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var html = Encoding.UTF8.GetString(buffer, offset, count);
            _html.Append(html);
            return Completed.Task;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                using (_responseStream) { }
                
            }
            base.Dispose(disposing);
        }
    }

    internal static class Completed
    {
        public static readonly Task Task = Create();

        private static Task Create()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }
    }
}