namespace PersonRegistration.Domain.Core
{
    public class DomainNotification
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; }

        public string Value { get; }


    }
}
