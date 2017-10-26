using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Mvc;
using AutoMapper;
using Sana.WebShop.Dto;
using Sana.WebShop.Infrastructure.Data;
using Sana.WebShop.Models;

namespace Sana.WebShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        private readonly IMapper _mapper;

        private MemoryCache _cache = MemoryCache.Default;

        public ProductController(IMapper mapper, ProductRepository productRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: Product
        public ActionResult Index()
        {
            _productRepository.Storage = GetStorage();

            var products = _productRepository.List();

            ViewBag.Products = _mapper.Map(products, new List<ProductDto>());

            return View();
        }
        
        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductDto product)
        {
            try
            {
                _productRepository.Storage = GetStorage();

                _productRepository.Create(_mapper.Map(product, new Product()));

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View();
            }
        }

        
        public ActionResult Storage(string storage)
        {
            if (_cache.Contains("storage"))
            {
                _cache["storage"] = string.IsNullOrEmpty(storage) ? "Memory" : storage;
            }
            else
            {
                _cache.Add("storage", string.IsNullOrEmpty(storage) ? "Memory" : storage, DateTimeOffset.UtcNow.AddMinutes(1));
            }
            
            return RedirectToAction("Index");
        }

        private string GetStorage()
        {
            var _default = "Memory";

            return _cache.Contains("storage") ? _cache["storage"].ToString() : _default;
        }
    }
}
