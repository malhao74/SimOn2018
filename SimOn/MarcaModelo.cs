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
    class MarcaModelo : Marca, IEquatable<MarcaModelo>
    {
        [ExcelColumn("MODELO")]
        public string descricaoModelo { get; set; }

        public MarcaModelo() { }

        public bool Equals(MarcaModelo other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return descricaoMarca.Equals(other.descricaoMarca) && descricaoModelo.Equals(other.descricaoModelo);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {

            //Get hash code for the modelo field if it is not null. 
            int hashMarca = descricaoMarca == null ? 0 : descricaoMarca.GetHashCode();

            //Get hash code for the modelo field if it is not null. 
            int hashModelo = descricaoModelo == null ? 0 : descricaoModelo.GetHashCode();

            //Calculate the hash code for the product. 
            return hashMarca ^ hashModelo;
        }

    }
}
