using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimApi.Base;

public static class RedisHelper
{
    private static IServer server = null;
    private static IDatabase database { get; set; }


    public RedisHelper()
    {

    }


}
