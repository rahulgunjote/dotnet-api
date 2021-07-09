namespace Catalog.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString { 
            get {
               return $"mongodb://{User}:{Password}@{Host}:{Port}";
               //return "mongodb+srv://root-node:omD5Paeviacso4uu@cluster0.91ff9.mongodb.net";
            } 

           // mongodb+srv://root-node:<password>@cluster0.91ff9.mongodb.net/myFirstDatabase?retryWrites=true&w=majority
        }
    }
}