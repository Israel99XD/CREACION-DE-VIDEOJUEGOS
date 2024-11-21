using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConexionBD
{
    IMongoCollection<BsonDocument> ObtenerColeccion();
}
