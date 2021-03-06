using System.Configuration;
using System.Text.RegularExpressions;
using CsQuery;

namespace Tweaker
{
    using System.Diagnostics;

    public class ImagesFromCdn : ITweak
    {
        private const string CdnImagePatternKey = "tweaker:CdnImgRegex";
        private const string CdnBaseKey = "tweaker:CdnBase";
        private readonly Regex _srcRegex;
        private readonly string _cdnBase;

        public ImagesFromCdn(string srcRegex, string cdnBase)
        {
            _srcRegex = new Regex(srcRegex);
            _cdnBase = cdnBase;
        }

        public void Run(CQ doc)
        {
            try
            {
                doc["img"].Each(img =>
                {
                    var src = img.GetAttribute("src");
                    img.SetAttribute("src", _srcRegex.Replace(src, _cdnBase));
                });
            }
            catch (System.Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public static ImagesFromCdn TryCreateFromAppSettings()
        {
            var cdnRegex = ConfigurationManager.AppSettings[CdnImagePatternKey];
            var cdnBase = ConfigurationManager.AppSettings[CdnBaseKey];
            return string.IsNullOrWhiteSpace(cdnBase) || string.IsNullOrWhiteSpace(cdnRegex)
                ? null
                : new ImagesFromCdn(cdnRegex, cdnBase);
        }
    }
}