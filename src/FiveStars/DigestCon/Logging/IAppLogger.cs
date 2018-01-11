using System;

namespace DigestCon.Logging
{
    public interface IAppLogger<T>
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
        void Trace(string message);


        void Info(string message, Exception ex);
        void Debug(string message, Exception ex);
        void Error(string message, Exception ex);
        void Fatal(string message, Exception ex);
        void Trace(string message, Exception ex);
    }
}