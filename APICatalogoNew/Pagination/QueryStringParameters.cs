﻿namespace APICatalogoNew.Pagination;

public abstract class QueryStringParameters
{
    const int maxPageSize = 50;
    private int _pageSize = 10;

    public int PageNamber { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
}