using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using VerifyAdharApi.Models;

namespace VerifyAdharApi.Services
{
    public class MigrantService
    {
        private readonly IMongoCollection<Migrant> _migrants;

        public MigrantService(IMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _migrants = database.GetCollection<Migrant>(settings.CollectionName);
        }

        public List<Migrant> Get() =>
            _migrants.Find(migrant => !string.IsNullOrEmpty(migrant.Id)).ToList();

        public Migrant Get(string id) =>
            _migrants.Find<Migrant>(migrant => migrant.Id == id).FirstOrDefault();

        public Migrant Create(Migrant migrant)
        {
            _migrants.InsertOne(migrant);
            return migrant;
        }

        public void Update(string id, Migrant migrant) =>
            _migrants.ReplaceOne(migrant => migrant.Id == id, migrant);

        public void Remove(Migrant migrant) =>
            _migrants.DeleteOne(migrant => migrant.Id == migrant.Id);

        public void Remove(string id) =>
            _migrants.DeleteOne(mingrant => mingrant.Id == id);
    }
}