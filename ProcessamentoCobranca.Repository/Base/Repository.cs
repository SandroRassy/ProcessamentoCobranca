﻿using MongoDB.Driver;
using ProcessamentoCobranca.Domain.Interfaces;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Repository.Base
{
    public abstract class Repository<T> : IRepository<T> where T : IEntity
    {
        public readonly IMongoCollection<T> _collectionName;

        protected Repository(IMongoCollection<T> collectionName)
        {
            _collectionName = collectionName;
        }

        protected Repository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
        {
            _collectionName = connectionFactory.GetDatabase(databaseName).GetCollection<T>(collectionName);

        }

        public void Insert(T obj)
        {
            _collectionName.InsertOne(obj);
        }

        public T Query(Guid key)
        {
            return _collectionName.AsQueryable<T>().FirstOrDefault(w => w.Key == key);
        }        

        public IQueryable<T> QueryAll()
        {
            return _collectionName.AsQueryable<T>();
        }
    }
}
