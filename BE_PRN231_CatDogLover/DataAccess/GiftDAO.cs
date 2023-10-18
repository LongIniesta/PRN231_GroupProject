﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GiftDAO
    {
        private static GiftDAO instance = null;
        private static readonly object instanceLock = new object();
        private GiftDAO() { }
        public static GiftDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new GiftDAO();
                    }
                    return instance;
                }
            }
        }
        public Gift GetByID(string id)
        {
            Gift result = null;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Gifts.SingleOrDefault(u => u.GiftId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public Gift AddGift(Gift Gift)
        {
            Gift result;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Gifts.Add(Gift).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Gift RemoveGift(string id)
        {
            Gift result;
            Gift Gift = GetByID(id);
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Gifts.Remove(Gift).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Gift UpdateGift(Gift Gift)
        {
            Gift result;
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Gifts.Update(Gift).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Gift> GetAll()
        {
            List<Gift> result = new List<Gift>();
            try
            {
                var DBContext = new CatDogLoverContext();
                result = DBContext.Gifts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
