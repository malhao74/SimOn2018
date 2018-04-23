using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqToExcel.Attributes;

namespace SimOn
{
    // The IEquatable protocol is needed for the list distinct usage.
    class Marca : IEquatable<Marca>
    {
        [ExcelColumn("MARCA")]
        public string descricaoMarca { get; set; }

        public Marca() { }

        public bool Equals(Marca other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return descricaoMarca.Equals(other.descricaoMarca);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashMarca = descricaoMarca == null ? 0 : descricaoMarca.GetHashCode();


            //Calculate the hash code for the product. 
            return hashMarca;
        }
    }
}
