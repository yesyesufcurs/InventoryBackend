// <copyright file="ItemsController.cs" company="Vincent (yesyesufcurs)">
// Copyright (c) Vincent (yesyesufcurs). All rights reserved.
// </copyright>

namespace InventoryV3.Service.Controllers
{
    using InventoryV3.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsController"/> class.
        /// </summary>
        /// <param name="dataContext">DataContext of the database.</param>
        public ItemsController(DataContext dataContext)
        {
            _context = dataContext;

            _context.AddRange(items);
            _context.SaveChanges();
        }

        private static List<Item> items = new List<Item>
            {
                new Item
                {
                    Name = "Toothbrush",
                    DateOfPurchase = DateTime.Now,
                    Barcode = "983228238FF",
                },
            };

        [HttpGet]
        public async Task<ActionResult<List<Item>>> Get()
        {
            return Ok(await _context.Items.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var item = items.Find(x => x.Id == id);
            if (item is null)
            {
                return BadRequest("Item not found");
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<List<Item>>> AddItem(Item item)
        {
            items.Add(item);
            return Ok(items);
        }

        [HttpPut]
        public async Task<ActionResult<List<Item>>> UpdateItem(Item request)
        {
            var item = items.Find(x => x.Id == request.Id);

            item.Name = request.Name;
            item.DateOfPurchase = request.DateOfPurchase;
            item.Barcode = request.Barcode;

            return Ok(items);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(int id)
        {
            var item = items.Find(x => x.Id == id);
            if (item is null)
            {
                return BadRequest("Item not found");
            }

            items.Remove(item);
            return Ok(item);
        }

    }
}
