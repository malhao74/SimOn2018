using System;
using System.Net.Http;

namespace SimOn
{
    class FirebaseDB
    {
        private string rootNode;

        public FirebaseDB(string baseUrl)
        { this.rootNode = baseUrl; }

        public FirebaseDB Node(string node)
        {
            if (node.Contains("/"))
            {
                throw new FormatException("Node must not contain '/', use NodePath insted.");
            }
            return new FirebaseDB(this.rootNode + '/' + node);
        }

        public FirebaseDB NodePath(string nodePath)
        {
            return new FirebaseDB(this.rootNode + '/' + nodePath);
        }
        public FirebaseResponse Get()
        {
            return new FirebaseRequest(HttpMethod.Get, this.rootNode).Execute();
        }

        public FirebaseResponse Put(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Put, this.rootNode, jsonData).Execute();
        }

        public FirebaseResponse Patch(string jsonData)
        {
            return new FirebaseRequest(new HttpMethod("PATCH"), this.rootNode, jsonData).Execute();
        }

        public FirebaseResponse Delete()
        {
            return new FirebaseRequest(HttpMethod.Delete, this.rootNode).Execute();
        }

        public override string ToString()
        {
            return this.rootNode;
        }
    }
}
