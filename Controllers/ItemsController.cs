using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Catalog.Entities;
using System;
using Catalog.Repositories;
using System.Linq;
using Catalog.DTOs;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Catalog.Controllers
{  

    //GET /items
    [ApiController]
    [Route("[controller]")]
    public class ItemsController: ControllerBase
    {
        private readonly IItemsRepository repository;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(IItemsRepository repository, ILogger<ItemsController> logger) {
            this.repository = repository;
            this.logger = logger;
        }
        
        //GET /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                        .Select(item => item.AsDto());

            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items.Count()}");
            return items;
        }

         //GET /item
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {   
            var item = await repository.GetItemAsync(id);
            if (item is null) {
                return NotFound();
            }
            return item.AsDto();
        }
        
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto) {
               
               Item item = new() {
                   Id = Guid.NewGuid(),
                   Name = itemDto.Name,
                   Price = itemDto.Price,
                   CreatedDate = DateTimeOffset.UtcNow
               };

               await repository.CreateItemAsync(item);
               return CreatedAtAction(nameof(GetItemAsync), new {Id = item.Id}, item.AsDto());
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto) {
             var existingItem = await repository.GetItemAsync(id);
             if(existingItem is null) {
                 return NotFound();
             }
             Item updatedItem = existingItem with {
                 Name = itemDto.Name,
                 Price = itemDto.Price
             };

             await repository.UpdateItemAsync(updatedItem);

             return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id){
             var existingItem = repository.GetItemAsync(id);
             if(existingItem is null) {
                 return NotFound();
             }
             await repository.DeleteItemAsync(id);
             return NoContent();
        }
    }

}