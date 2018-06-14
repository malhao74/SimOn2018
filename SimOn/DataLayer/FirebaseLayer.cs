using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Auth;


namespace SimOn
{
    /// <summary>
    /// Implementation of methods to connect to firebase database and retrive information.
    /// </summary>
    class FirebaseLayer
    {

        public FirebaseLayer()
        {

            //FirebaseLayer firebase = new FirebaseLayer();
            FirebaseDB firebaseDB = new FirebaseDB("https://simon-4288d.firebaseio.com");
            FirebaseDB firebaseMarcas = firebaseDB.Node("");

            var data = @"{ 'viaturas': {
                            'FIAT': {
                            'PUNTO': {
                                              'x xx': {
                                                        'Preco' : '15000'
                                                        },
                                              'xxx xxx': {
                                                        'Preco' : '23000'
                                                        },
                                              'xxx xxx cddff': {
                                                        'Preco' : '23000'
                                                        }
                                              }
                                    }
                           }
                    }";

            FirebaseResponse putResponse = firebaseMarcas.Put(data);
            Console.Write(putResponse);
            //,
            //                        '2.0 D' : {
            //    'marca' : 'FIAT','modelo' : 'PUNTO', 'versao' : '2.0 D', 'preco' : '23000'
            //                                  }

            var dataGet = @"{}";
            Console.WriteLine("GET Request!");
            FirebaseResponse getResponse = firebaseMarcas.Get();
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success)
            { Console.WriteLine(getResponse.JSONContent); }




            /*
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var auth = authProvider.SignInWithEmailAndPasswordAsync("luis.malhao@gmail.com", "xxxxxx");

            // finally log in
            var firebaseClient = new FirebaseClient(
                                    databaseURL,
                                    new FirebaseOptions
                                    {
                                        AuthTokenAsyncFactory = () => Task.FromResult(auth.Result)
                                    });

            // now you can make your query
            var results = await firebaseClient
                            .Child("users")
                            .OnceAsync();


            //Firebase.Auth.IFirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(this.apiKey));
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            //Firebase.Auth.IFirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var token = auth.SignInAnonymouslyAsync(); //.SignInWithEmailAndPasswordAsync("luis.malhao@gmail.com", "xxxxxx");
            /*
            var firebaseClient = new FirebaseClient(
                        firebaseUrl,
                        new FirebaseOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult( token.Result<FirebaseAuthLink>)
                        });
            //auth.Start();

            
            var firebase = new FirebaseClient(
              this.databaseURL,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth.Result)
              });

            var dinos = await firebase
              .Child("dinosaurs")
              .OnceAsync<Dinosaur>();

            foreach (var dino in dinos)
            {
                Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high.");
            }
            */

        }
    }
}
