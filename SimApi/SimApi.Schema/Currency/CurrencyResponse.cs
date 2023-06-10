﻿using SimApi.Base;

namespace SimApi.Schema;

public class CurrencyResponse : BaseResponse
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}
