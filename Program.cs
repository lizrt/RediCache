using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SportsStatsTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config["CacheConnection"];

            using (var cache = ConnectionMultiplexer.Connect(connectionString))
            {
                IDatabase db = cache.GetDatabase();

                bool setValue = db.StringSet("test:key", "some value");
                Console.WriteLine($"SET: {setValue}");

                string getValue = db.StringGet("test:key");
                Console.WriteLine($"GET: {getValue}");
                List<Person> plist =new List<Person>();
                plist.Add(new Person(){name="Alice",address="address 111",age=30});
                plist.Add(new Person(){name="Sam",address="address 113",age=40});
                plist.Add(new Person(){name="Jason",address="address 44",age=50});
                plist.Add(new Person(){name="Ann",address="address 55",age=20});
                 setValue = db.StringSet("test:personobj", JsonConvert.SerializeObject(plist));
                Console.WriteLine($"SET: {setValue}");
 				 getValue =  db.StringGet("test:personobj");
				Console.WriteLine($"GET: {JsonConvert.DeserializeObject(getValue)}");

            }
        }
    }
    class Person{
        public string name{get;set;}
        public int age {get;set;}
        public string address{get;set;}
    }
}