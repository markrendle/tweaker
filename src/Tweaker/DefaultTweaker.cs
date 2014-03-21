namespace Tweaker
{
    using System;
    using System.Web;

    public class DefaultTweaker : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.ReleaseRequestState += InstallFilter;
        }

        public void Dispose()
        {
        }

        private void InstallFilter(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;

            if (response.ContentType.Equals("text/html", StringComparison.OrdinalIgnoreCase))
            {
                response.Filter = new FilterStream(response.Filter, HtmlTweaker.Default);
            }
        }
    }
}