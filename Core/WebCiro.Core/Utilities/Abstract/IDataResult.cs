﻿namespace WebCiro.Core.Utilities.Abstract
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; set; }
    }
}
