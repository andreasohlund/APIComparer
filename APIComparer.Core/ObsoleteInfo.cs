namespace APIComparer
{
    public class ObsoleteInfo
    {
        public ObsoleteInfo(bool asError, string message)
        {
            AsError = asError;
            RawMessage = message;
            Message = CleanupMessage(message);
        }

        public bool AsError { get;}

        public string RawMessage { get; }

        public string TargetVersion
        {
            get
            {
                if (AsError)
                {
                    return "Current";
                }

                var start = RawMessage.IndexOf(ERROR_FROM_VERSION);

                if (start < 0)
                {
                    return "Non specified future version";
                }

                return RawMessage.Substring(start+ERROR_FROM_VERSION.Length, 6).Trim();
            }
        }

        public string Message { get; }

        string CleanupMessage(string rawMessage)
        {
            var message = CleanupRemoveInVersion(rawMessage);

            return CleanupTreatAsErrorInVersion(message);
        }

        string CleanupRemoveInVersion(string rawMessage)
        {
            var trimStart = rawMessage.IndexOf(REMOVE_IN_VERSION);

            if (trimStart > 0)
            {
                return rawMessage.Substring(0, trimStart).Trim();
            }

            return rawMessage;
        }
        string CleanupTreatAsErrorInVersion(string rawMessage)
        {
            var trimStart = rawMessage.IndexOf(ERROR_FROM_VERSION);

            if (trimStart > 0)
            {
                return rawMessage.Substring(0, trimStart).Trim();
            }

            return rawMessage;
        }

        const string ERROR_FROM_VERSION = "Will be treated as an error from version";
        const string REMOVE_IN_VERSION = "Will be removed in version";
    }
}