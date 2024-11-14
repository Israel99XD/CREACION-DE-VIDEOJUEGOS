using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConexionBD
{

    public IMongoCollection<BsonDocument> ConexionMongo()
    {
        var client = new MongoClient("mongodb+srv://seriscompany:cPsPUFaEUiMoOOdb@cluster0.pnfekrf.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
        var database = client.GetDatabase("dbJuego");
        var coleccion = database.GetCollection<BsonDocument>("Users");

        return coleccion;
    }

    public IMongoCollection<BsonDocument> ConexionMongo2()
    {
        var client = new MongoClient("mongodb+srv://seriscompany:cPsPUFaEUiMoOOdb@cluster0.pnfekrf.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
        var database = client.GetDatabase("dbJuego");
        var coleccion = database.GetCollection<BsonDocument>("Score");

        return coleccion;
    }
}

