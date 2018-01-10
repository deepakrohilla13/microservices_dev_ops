using System;
using DigestCon.Logging;

namespace DigestCon
{
    public class DocBuilder : IDocBuilder
    {
        IAppLogger<DocBuilder> _appLogger;
        public DocBuilder(IAppLogger<DocBuilder> appLogger)
        {
            _appLogger = appLogger;
        }

        public void Try()
        {
            _appLogger.Debug("DDDDDDDD"+" "+DateTime.Now);
            _appLogger.Error("EEEEEEEE"+" "+DateTime.Now);
            _appLogger.Info("IIIIIIIIII"+" "+DateTime.Now);
            _appLogger.Trace("TTTTTTTT"+" "+DateTime.Now);
            _appLogger.Fatal("FFFFFFFFF"+" "+DateTime.Now);
        }
    }
}