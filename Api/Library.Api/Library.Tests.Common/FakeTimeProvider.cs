using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Common
{
    public class FakeTimeProvider:TimeProvider
    {
        private DateTimeOffset _dateTime;
        public override DateTimeOffset GetUtcNow()
        {
            return base.GetUtcNow();
        }
        public void SetUtcNow(DateTimeOffset dateTime) { 
        _dateTime=dateTime;
        }
        public override long GetTimestamp()
        {
            return _dateTime.ToUnixTimeMilliseconds();
        }
    }
}
