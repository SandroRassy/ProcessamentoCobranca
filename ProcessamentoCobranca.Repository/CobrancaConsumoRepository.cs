﻿using MongoDB.Driver;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Base;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Repository
{
    public sealed class CobrancaConsumoRepository : Repository<CobrancaConsumo>, ICobrancaConsumoRepository
    {
        public CobrancaConsumoRepository(IMongoCollection<CobrancaConsumo> collectionName) : base(collectionName)
        {
        }

        public CobrancaConsumoRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }
    }
}