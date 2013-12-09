using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using HummingbirdSlugDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HummingbirdSlugDb
{
    public class Dal : IDisposable
    {
        private MongoServer mongoServer = null;
        private bool disposed = false;


        private string connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_MONGOLAB_URI");


        private string dbName = "MongoLab-ec";
        private string collectionName = "HummingbirdMappings";


        // Creates a Note and inserts it into the collection in MongoDB.
        public bool InsertMapping(params Mapping[] mappings)
        {
            var collection = GetMappings();
            try
            {
                foreach (var mapping in mappings)
                {
                    collection.Update(Query.EQ("TvDBId", mapping.TvDBId),
                                      Update.Replace(mapping),
                                      UpdateFlags.Upsert);
                    return true;
                }
            }
            catch (MongoCommandException ex)
            {
                
            }
            return false;
        }

        public IEnumerable<Mapping> RetrieveMappings(params int[] tvDbIds)
        {
            var collection = GetMappings();

            if (!tvDbIds.Any()) return collection.FindAll();

            var retrievedMappings = collection.FindAs<Mapping>(Query<Mapping>.In(x => x.TvDBId, tvDbIds));
            return retrievedMappings.Any() ? retrievedMappings.ToList() : (new Mapping[0]).ToList();
        }

        private MongoCollection<Mapping> GetMappings()
        {
            var client = (ConfigurationManager.AppSettings["onAzure"] == "true") ? new MongoClient(connectionString) : new MongoClient();

            var database = client.GetServer()[dbName];
            return database.GetCollection<Mapping>(collectionName);
        }


        # region IDisposable


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (mongoServer != null)
                    {
                        mongoServer.Disconnect();
                    }
                }
            }


            disposed = true;
        }


        # endregion
    }
}