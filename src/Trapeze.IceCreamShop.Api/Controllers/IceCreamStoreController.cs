// <copyright file="IceCreamStoreController.cs" company="Trapeze Ice Cream">
// Copyright (c) Trapeze Ice Cream. All rights reserved.
// </copyright>

namespace Trapeze.IceCreamShop.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Trapeze.IceCreamShop.Abstractions;
    using Trapeze.IceCreamShop.Models;

    /// <summary>
    /// An API controller for performing actions on the ice cream store.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class IceCreamStoreController : ControllerBase
    {
        private readonly IIceCreamShopService _iceCreamShopService;
        /// <summary>
        /// Initializes a new instance of the <see cref="IceCreamStoreController"/> class.
        /// </summary>
        public IceCreamStoreController(IIceCreamShopService iceCreamShopService)
        {
            _iceCreamShopService = iceCreamShopService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> PurchaseIceCream([FromBody] IceCreamPurchasedRequest request)
        {
            var result = await _iceCreamShopService.ProcessRequest(request).ConfigureAwait(false);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
