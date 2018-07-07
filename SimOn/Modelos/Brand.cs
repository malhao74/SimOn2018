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
    /// This class handles the brand data model. The IEquatable protocol is needed for the list distinct usage
    /// </summary>
    internal class Brand : IEquatable<Brand>
    {
        [ExcelColumn("BRAND")]  // For excel usage
        [JsonProperty("descricaoMarca")] // For Firebase usage
        public string BrandDescription { get; set; }

        public int BrandId { get; set; } // For Firebase usage

        public Brand() { }

        public bool Equals(Brand other)
        {

            //Check whether the compared object is null. 
            //if (Object.ReferenceEquals(other, null)) return false;
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
            return BrandDescription.Equals(other.BrandDescription) && BrandId.Equals(other.BrandId);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashMarca = BrandDescription == null ? 0 : BrandDescription.GetHashCode();

            //int hashIdMarca = IdMarca.GetHashCode();

            //Calculate the hash code for the product. 
            return hashMarca; //^ hashIdMarca;
        }
    }
}
