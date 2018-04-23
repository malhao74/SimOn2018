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
        private string apiKey= "AIzaSyCnoDFAFv-O2_pjssigH4ck-n8gzMZ9L5U";
        private string authDomain = "simon-4288d.firebaseapp.com";
        private string databaseURL = "https://simon-4288d.firebaseio.com";
        private string projectId = "simon-4288d";
        private string storageBucket = "simon-4288d.appspot.com";
        private string messagingSenderId = "993821413586";

        public FirebaseLayer()
        {





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
