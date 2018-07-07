using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqToExcel.Attributes;
using Newtonsoft.Json;

namespace SimOn
{
    /// <summary>
    /// This class models the data structure of the model of the car.The IEquatable protocol is needed for the list distinct usage 
    /// </summary>
    internal class Model : Brand, IEquatable<Model>
    {
        [ExcelColumn("MODEL")] // For excel usage
        [JsonProperty("descricaoModelo")] // For Firebase usage
        public string ModelDescription { get; set; }
        //Para uso com firebase
        public int ModelId { get; set; }
        public int Brand { get; set; }

        public Model() { }

        public bool Equals(Model other)
        {

            //Check whether the compared object is null. 
            if (other is null)
            {
                return false;
            }

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            //Check whether the products' properties are equal. 
            return BrandDescription.Equals(other.BrandDescription) && ModelDescription.Equals(other.ModelDescription);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {

            //Get hash code for the modelo field if it is not null. 
            int hashMarca = BrandDescription == null ? 0 : BrandDescription.GetHashCode();

            //Get hash code for the modelo field if it is not null. 
            int hashModelo = ModelDescription == null ? 0 : ModelDescription.GetHashCode();

            //Calculate the hash code for the product. 
            return hashMarca ^ hashModelo;
        }

    }
}
