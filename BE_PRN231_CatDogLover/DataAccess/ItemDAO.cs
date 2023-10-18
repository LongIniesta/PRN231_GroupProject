﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ItemDAO
    {
        private static ItemDAO instance = null;
        private static readonly object instanceLock = new object();
        private ItemDAO() { }
        public static ItemDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemDAO();
                    }
                    return instance;
                }
            }
        }
        public Item GetByID(string id)
        {
            Item result = null;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Items.SingleOrDefault(u => u.ItemId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public Item AddItem(Item Item)
        {
            Item result;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Items.Add(Item).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Item RemoveItem(string id)
        {
            Item result;
            Item Item = GetByID(id);
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Items.Remove(Item).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Item UpdateItem(Item Item)
        {
            Item result;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Items.Update(Item).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Item> GetAll()
        {
            List<Item> result = new List<Item>();
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Items.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
