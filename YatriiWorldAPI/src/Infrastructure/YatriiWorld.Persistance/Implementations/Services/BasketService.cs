using AutoMapper;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Basket;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IProductRepository productRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task AddItemToBasketAsync(long userId, long productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || product.StockQuantity < quantity)
                throw new Exception("Product not found");

            var basket = await _basketRepository.GetBasketByUserIdAsync(userId);

            if (basket == null)
            {
                basket = new Basket { UserId = userId, Items = new List<BasketItem>() };
                await _basketRepository.AddAsync(basket);
            }

            var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                basket.Items.Add(new BasketItem { ProductId = productId, Quantity = quantity });
            }

            await _basketRepository.SaveAsync();
        }
        public async Task<BasketDto> GetUserBasketAsync(long userId)
        {
            var basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (basket == null)
            {
                return new BasketDto { UserId = userId, Items = new List<BasketItemDto>() };
            }
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task RemoveItemFromBasketAsync(long userId, long productId)
        {
            var basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (basket == null) return;

            var itemToRemove = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                basket.Items.Remove(itemToRemove);
                await _basketRepository.SaveAsync();
            }
        }

        public async Task ClearBasketAsync(long userId)
        {
            var basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (basket != null)
            {
                basket.Items.Clear();
                await _basketRepository.SaveAsync();
            }
        }
    }
}