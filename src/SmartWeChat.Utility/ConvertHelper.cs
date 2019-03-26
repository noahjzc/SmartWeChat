using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartWeChat.Utility
{
  public static  class ConvertHelper
    {
        public static string StreamToString(this Stream value)
        {
            var rtn = new List<byte>();
            var buffer = new byte[100];
            var offset = 0;
            var totalCount = 0;
            while (true)
            {
                int bytesRead = value.Read(buffer, 0, 100);
                if (bytesRead == 0)
                {
                    break;
                }
                offset += bytesRead;
                totalCount += bytesRead;
                rtn.AddRange(buffer.Take(bytesRead));
            }

            return Encoding.UTF8.GetString(rtn.ToArray());
        }
    }
}
