using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Catalog.Entities;
using System;
using Catalog.Repositories;
using System.Linq;
using Catalog.DTOs;

namespace Catalog.Controllers
{  

    //GET /items
    [ApiController]
    [Route("[controller]")]
    public class ItemsController: ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository) {
            this.repository = repository;
        }
        
        //GET /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

         //GET /item
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {   
            var item = repository.GetItem(id);
            if (item is null) {
                return NotFound();
            }
            return item.AsDto();
        }
    }

}