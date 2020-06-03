using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using VerifyAdharApi.Models;

namespace VerifyAdharApi.Services
{
    public class MigrantService
    {
        private readonly IMongoCollection<Migrant> _migrants;
        private readonly Dictionary<long, Dictionary<decimal, decimal>> _coordinatesData;

        public MigrantService(IMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _migrants = database.GetCollection<Migrant>(settings.CollectionName);
            _coordinatesData = GetLatitudeLongitudeInfo();
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

        public Coordinates GetLatitudeLongitudeWithMigrantsCount(long pincode)
        {
            var coordinates = _coordinatesData.FirstOrDefault(t => t.Key == pincode).Value;
            var migrants = Get().Where(t => t.PinCode == pincode).Count();
            var data = new Coordinates();
            data.Latitude = coordinates.FirstOrDefault().Key;
            data.Longitude = coordinates.FirstOrDefault().Value;
            data.Count = migrants;
            return data;
        }

        private Dictionary<long, Dictionary<decimal, decimal>> GetLatitudeLongitudeInfo()
        {
            var info = new Dictionary<long, Dictionary<decimal, decimal>>();
            using (TextFieldParser parser = new TextFieldParser(@"Resources\Latitute_Longitude.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    var latitudeLogitude = new Dictionary<decimal, decimal>();
                    latitudeLogitude.Add(Convert.ToDecimal(fields[1]), Convert.ToDecimal(fields[2]));
                    info.Add(Convert.ToInt64(fields[0]), latitudeLogitude);
                }
            }
            return info;
        }
    }
}