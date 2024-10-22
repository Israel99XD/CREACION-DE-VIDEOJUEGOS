using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConexionBD
{

    public IMongoCollection<BsonDocument> ConexionMongo()
    {
        var client = new MongoClient("mongodb+srv://tonathiu123456:KE2jkZdUzPo9BOK9@invacioncanina.sksayaa.mongodb.net/?retryWrites=true&w=majority");
        var database = client.GetDatabase("dbJuego");
        var coleccion = database.GetCollection<BsonDocument>("Users");

        return coleccion;
    }

    public IMongoCollection<BsonDocument> ConexionMongo2()
    {
        var client = new MongoClient("mongodb+srv://tonathiu123456:KE2jkZdUzPo9BOK9@invacioncanina.sksayaa.mongodb.net/?retryWrites=true&w=majority");
        var database = client.GetDatabase("dbJuego");
        var coleccion = database.GetCollection<BsonDocument>("Score");

        return coleccion;
    }
}

