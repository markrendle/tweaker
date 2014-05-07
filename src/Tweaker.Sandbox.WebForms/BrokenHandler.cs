namespace Tweaker.Sandbox.WebForms
{
    using System.Web;

    public class BrokenHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "text/html";
            response.Write("<h1>This is broken</h1>");
        }

        public bool IsReusable { get { return true; } }
    }
}