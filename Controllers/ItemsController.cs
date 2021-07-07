using Microsoft.AspNetCore.Mvc;
using Catalog.Repositores;
using System.Collections.Generic;
using Catalog.Entities;
using System;

namespace Catalog.Controllers
{  

    //GET /items
    [ApiController]
    [Route("[controller]")]
    public class ItemsController: ControllerBase
    {
        private readonly InMemItemsRepository repository;

        public ItemsController() {
            repository = new InMemItemsRepository();
        }
        
        //GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }

         //GET /item
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {   
            var item = repository.GetItem(id);
            if (item is null) {
                return NotFound();
            }
            return item;
        }
    }

}