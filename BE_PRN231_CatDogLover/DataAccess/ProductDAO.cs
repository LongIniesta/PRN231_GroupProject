﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public Product GetByID(string id)
        {
            Product result = null;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Products.SingleOrDefault(u => u.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public Product AddProduct(Product Product)
        {
            Product result;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Products.Add(Product).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Product RemoveProduct(string id)
        {
            Product result;
            Product Product = GetByID(id);
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Products.Remove(Product).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Product UpdateProduct(Product Product)
        {
            Product result;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Products.Update(Product).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Product> GetAll()
        {
            List<Product> result = new List<Product>();
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
