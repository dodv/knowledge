using System;
using System.Globalization;

namespace Knowledge.Api.Services
{
    public class DateTimeService
    {
        public virtual DateTimeOffset Now => DateTimeOffset.UtcNow;
        public string DateToISO8601(DateTimeOffset date)
        {
            return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        }
    }
}
