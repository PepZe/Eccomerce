﻿using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface IProductRepository : IPersistData<Product>, IRepository<Product>
    {
    }
}
