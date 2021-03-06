﻿using MongoDB.Bson;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.InfraStructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	public abstract class ProductServiceBase : IProductService
	{
		public ProductServiceBase(IRepository<Product> repository)
		{
			this.Repository = repository;
		}
		public IRepository<Product> Repository { get; private set; }

		public async Task<Product> SaveProductAsync(string id, Product product)
		{
			if (string.IsNullOrEmpty(id))
			{
				await Repository.InsertOneAsync(product);
			}
			else
			{
				await Repository.ReplaceOneAsync(product);
			}

			return product;
		}

		public virtual async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await Repository.GetAllAsync();
		}

		public async Task<Product> GetByIdAsync(string id)
		{
			return await Repository.FindByIdAsync(id);
		}

		public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<ObjectId> ids)
		{
			return await Repository.FilterByAsync(w => ids.Contains(w.Id));
		}
	}
}
