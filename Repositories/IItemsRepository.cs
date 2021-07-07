using System.Collections.Generic;
using Catalog.Entities;
using System;
namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}