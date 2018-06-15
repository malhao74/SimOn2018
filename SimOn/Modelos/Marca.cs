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
    internal class Marca : IEquatable<Marca>
    {
        [ExcelColumn("MARCA")]
        public string DescricaoMarca { get; set; }
        //Para uso com firebase
        public int IdMarca { get; set; }

        public Marca() { }

        public bool Equals(Marca other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return DescricaoMarca.Equals(other.DescricaoMarca) && IdMarca.Equals(other.IdMarca);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashMarca = DescricaoMarca == null ? 0 : DescricaoMarca.GetHashCode();

            int hashIdMarca = IdMarca.GetHashCode();

            //Calculate the hash code for the product. 
            return hashMarca ^ hashIdMarca;
        }
    }
}
