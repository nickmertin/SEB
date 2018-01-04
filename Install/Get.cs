using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Install
{
    static class Get
    {
        static public Stream Installer(string key)
        {
            TcpClient c = new TcpClient("einsteinjunior.mshome.net", 1732);
            c.GetStream().Write(Encoding.UTF8.GetBytes(key), 0, 8);
            if (c.GetStream().ReadByte() == 1)
                return c.GetStream();
            return null;
        }
    }
}
